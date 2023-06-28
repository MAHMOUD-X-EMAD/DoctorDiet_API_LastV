using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.DTO;
using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Repositories.Interfaces;
using AutoMapper.QueryableExtensions;
using DoctorDiet.Repository.Repositories;

namespace DoctorDiet.Services
{
    public class PatientService
    { 
    
        IGenericRepository<Patient, string> _Patientrepositry;
        IGenericRepository<DoctorPatientBridge, int> _repositryBridge;
        IGenericRepository<Doctor, string> _DoctorRepositry;
        IGenericRepository<DoctorPatientBridge, int> _doctorPatirentRepository;
        IMapper _mapper;
        NoEatService _NoEatService;
        AccountService _accountService;
        IUnitOfWork _unitOfWork;
        CustomPlanService _customPlanService;
        IPatientRepository _patientRepository;
        public PatientService(IGenericRepository<Patient, string> Repositry,
            IMapper mapper, NoEatService NoEatService, 
            AccountService accountService, IUnitOfWork unitOfWork
            , CustomPlanService customPlanService,
            IPatientRepository patientRepository,
            IGenericRepository<Doctor, string> doctorRepositry,
            IGenericRepository<DoctorPatientBridge, int> doctorPatirentRepository)
        {
            _Patientrepositry = Repositry;
            _mapper = mapper;
            _NoEatService = NoEatService;
            _accountService = accountService;
            _unitOfWork = unitOfWork;
            _customPlanService = customPlanService;
            _patientRepository = patientRepository;
            _repositryBridge = doctorPatirentRepository;
            _DoctorRepositry = doctorRepositry;
            _doctorPatirentRepository = doctorPatirentRepository;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _Patientrepositry.GetAll();
        }
        public Patient GetPatientData(string id)
        {
           Patient patient = _Patientrepositry.Get(o => o.Id == id)
                .Include(x => x.ApplicationUser)
                .Include(c=>c.CustomPlans)
                .ThenInclude(d=>d.DaysCustomPlan)
                .ThenInclude(m=>m.DayMealCustomPlanBridge)
                .ThenInclude(m=>m.MealCustomPlan)
                .FirstOrDefault();

            return patient;

        }
        public Patient AddPatient(RegisterPatientDto registerPatientDto)
        {
            Patient patient = _mapper.Map<Patient>(registerPatientDto);
            patient.Id = registerPatientDto.PatientId;
            _accountService.AddPatient(patient);

            foreach (string noEat in registerPatientDto.noEats)
            {
                NoEat noeat = new NoEat
                {

                    PatientId = patient.Id,
                    Name = noEat,

                };
                _NoEatService.AddNoEat(noeat);
            }

        

            

            return patient;

        }
        public List<List<DayCustomPlan>> GetEveryDayWithMealsOfDay(string patientID)
        {
            List<CustomPlan> customPlans = _customPlanService.GetPlans(d => d.PatientId == patientID).ToList();
            List<DayCustomPlan> day = new List<DayCustomPlan>();
            List<List<DayCustomPlan>> Alldays = new List<List<DayCustomPlan>>();
            foreach (var customPLan in customPlans)
            {
                day = customPLan.DaysCustomPlan;
                Alldays.Add(day);

            }

            return Alldays;

        }

        public List<ShowCustomPlanDto> GetPatientHistory(string patientID)
        {
            IQueryable<CustomPlan> customPlans = _customPlanService.GetPlans(c => c.PatientId == patientID);

            List<ShowCustomPlanDto> customPlanDtos = customPlans.ProjectTo<ShowCustomPlanDto>(_mapper.ConfigurationProvider).ToList();


            return customPlanDtos;
        }

    public List<GetPatientNoteData> GetPateintNotes(GetPatientNotesDTO getPatientNotesDTO)
    {
      IQueryable<PatientNotes> patientNotes = _patientRepository.GetNotes(getPatientNotesDTO);
      List<GetPatientNoteData> patientNotesDTO = _mapper.ProjectTo<GetPatientNoteData>(patientNotes).ToList();

      return patientNotesDTO;
    }

    public string Confirm(SubscribeDto subscribeDto)
        {
            Patient patient = _Patientrepositry.GetAll()
                .FirstOrDefault(pat => pat.Id == subscribeDto.PatientId);
           CustomPlan customPlan=  _customPlanService.AddCustomPlan(patient);
            if (customPlan != null)
            {
                _unitOfWork.SaveChanges();

                DoctorPatientBridge doctorPatientBridge = _doctorPatirentRepository.
                Get(d => d.DoctorID == subscribeDto.DoctorID && d.PatientID == subscribeDto.PatientId).
                FirstOrDefault();

                doctorPatientBridge.Status = Status.Confirmed;
                _doctorPatirentRepository.Update(doctorPatientBridge, nameof(DoctorPatientBridge.Status));
                _unitOfWork.SaveChanges();

                return (doctorPatientBridge.Status).ToString();
            }else
            {
                return string.Empty;
            }
        }

        public string GetStatus(string patientid)
        {
            DoctorPatientBridge doctorPatientBridges = _doctorPatirentRepository
                .Get(p => p.PatientID == patientid && p.Status == (Status)1)
                .FirstOrDefault();

            if (doctorPatientBridges != null)
            {
                return doctorPatientBridges.Status.ToString();
            }
            else
            {
                return "Not Confirmed in plan";
            }


        }

        public string Reject(SubscribeDto subscribeDto) 
        {
            string Status=_patientRepository.Reject(subscribeDto);
            _unitOfWork.SaveChanges();

            return Status;
        }

        public string CancelStatus(SubscribeDto subscribeDto)
        {
            DoctorPatientBridge doctorPatientBridges = _doctorPatirentRepository
                .Get(p => p.PatientID == subscribeDto.PatientId &&
                 p.DoctorID == subscribeDto.DoctorID && p.Status == (Status)1)
                .FirstOrDefault();

            doctorPatientBridges.Status = Status.Cancled;

            _doctorPatirentRepository.Update(doctorPatientBridges, nameof(DoctorPatientBridge.Status));
            _unitOfWork.SaveChanges();

            return (doctorPatientBridges.Status).ToString();


        }
        public PatientDTO Subscription(SubscribeDto subscribeDto)
        {
           PatientDTO patientDTO= _patientRepository.Subscription(subscribeDto);
            _unitOfWork.SaveChanges();

            return patientDTO;

        }

    public PatientDTO GetPatientDTO(string PatientId)
    {
      PatientDTO patientDTO = _patientRepository.GetPatientDTO(PatientId);
     
      Patient p = _patientRepository.GetByID(PatientId);
      patientDTO.CustomPlans = _mapper.Map<List<CustomPlanDTO>>(p.CustomPlans);
      _unitOfWork.SaveChanges();

      return patientDTO;

    }
    public UserDataDTO GetPatientDataDTO(string id)
        {
            Patient patient = GetPatientData(id);
            var patientdto = _mapper.Map<UserDataDTO>(patient);
            return patientdto;

        }

        public IEnumerable<PatientDTO> GetPatientsByDoctorIdWithStatusConfirmed(string doctorID)
        {

            var DoctorPatientsBridge = _repositryBridge
            .Get(p => p.DoctorID == doctorID && p.Status.Equals(Status.Confirmed))
            .Include(b=>b.Patient)
            .ThenInclude(p=>p.ApplicationUser);

            var patientDTOs = DoctorPatientsBridge.ProjectTo<PatientDTO>(_mapper.ConfigurationProvider);

            return patientDTOs;
        }

        public IEnumerable<PatientDTO> GetPatientsByDoctorIdWithStatusWaiting(string doctorID)
        {

            var DoctorPatientsBridge = _repositryBridge.Get(p => p.DoctorID == doctorID && p.Status.Equals(Status.Waiting));

            var patientDTOs = _mapper.ProjectTo<PatientDTO>(DoctorPatientsBridge);

            return patientDTOs;
        }
        

    public string AddNote(PatientNotesDTO patientNotesDto)
    {
      PatientNotes patientNotes = _mapper.Map<PatientNotes>(patientNotesDto);
      string Status = _patientRepository.AddNote(patientNotes);
      _unitOfWork.SaveChanges();

      return Status;
    }

        public void EditPatientData(EditPatientDto patientData, params string[] properties)
        {


            Patient patientMapper = _mapper.Map<Patient>(patientData);
            if (patientData.ProfileImage != null)
            {
                using var dataStream = new MemoryStream();
                patientData.ProfileImage.CopyTo(dataStream);
                         
                patientMapper.ApplicationUser.ProfileImage = dataStream.ToArray();
            }
            if (properties.Contains("Weight") || properties.Contains("ActivityRates"))
            {
                double BMR = 0.0;

                int MaxHisActivityRate = 0;
                int MinHisActivityRate = 0;

                double MinCals = 0.0;
                double MaxCals = 0.0;

                if (patientMapper.Gender == "Male")
                {
                    BMR = 24 * 1 * patientMapper.Weight;

                    if (patientMapper.ActivityRates.Contains("veryHigh"))
                    {
                        MaxHisActivityRate = 120;
                        MinHisActivityRate = 90;
                    }

                    else if (patientMapper.ActivityRates.Contains("high"))
                    {
                        MaxHisActivityRate = 80;
                        MinHisActivityRate = 65;
                    }

                    else if (patientMapper.ActivityRates.Contains("regular"))
                    {
                        MaxHisActivityRate = 70;
                        MinHisActivityRate = 50;
                    }

                    else if (patientMapper.ActivityRates.Contains("low"))
                    {
                        MaxHisActivityRate = 40;
                        MinHisActivityRate = 25;
                    }


                }

                else if (patientMapper.Gender == "Female")
                {
                    BMR = 24 * 0.9 * patientMapper.Weight;

                    if (patientMapper.ActivityRates.Contains("veryHigh"))
                    {
                        MaxHisActivityRate = 100;
                        MinHisActivityRate = 80;
                    }

                    else if (patientMapper.ActivityRates.Contains("high"))
                    {
                        MaxHisActivityRate = 70;
                        MinHisActivityRate = 50;
                    }

                    else if (patientMapper.ActivityRates.Contains("regular"))
                    {
                        MaxHisActivityRate = 60;
                        MinHisActivityRate = 40;
                    }

                    else if (patientMapper.ActivityRates.Contains("low"))
                    {
                        MaxHisActivityRate = 35;
                        MinHisActivityRate = 25;
                    }
                }

                MinCals = BMR * MinHisActivityRate / 100;
                MaxCals = BMR * MaxHisActivityRate / 100;

                patientMapper.MinCalories = (int)(MinCals + MaxCals);

                patientMapper.MaxCalories = (int)(MaxCals + MaxCals);


                Array.Resize(ref properties, properties.Length + 2);
                properties[properties.Length - 1] = "MinCalories";
                properties[properties.Length - 2] = "MaxCalories";


            }


            _Patientrepositry.Update(patientMapper, properties);
            _unitOfWork.SaveChanges();
        }

    }
}
