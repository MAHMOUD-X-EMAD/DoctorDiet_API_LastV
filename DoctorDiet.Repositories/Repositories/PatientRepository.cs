using AutoMapper;
using DoctorDiet.Data;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repositories.Interfaces;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repositories.Repositories
{
  public class PatientRepository : IPatientRepository
  {
    Context _context;
    ICustomPlanRepository _CustomPlanRepository;
    IMapper _mapper;
    IGenericRepository<DoctorPatientBridge, int> _doctorPatirentRepository;
    public PatientRepository(Context context, ICustomPlanRepository CustomPlanRepository,
    IGenericRepository<DoctorPatientBridge, int> doctorPatirentRepository,
IMapper mapper)
    {
      _context = context;
      _CustomPlanRepository = CustomPlanRepository;
      _mapper = mapper;
      _doctorPatirentRepository = doctorPatirentRepository;
    }


    public Patient Add(Patient entity)
    {
      _context.Add(entity);
      return entity;
    }
    public string Reject(SubscribeDto subscribeDto)
    {

      DoctorPatientBridge doctorPatientBridge = _doctorPatirentRepository.
         Get(d => d.DoctorID == subscribeDto.DoctorID && d.PatientID == subscribeDto.PatientId).
         FirstOrDefault();

      doctorPatientBridge.Status = Status.Rejected;


      _doctorPatirentRepository.Update(doctorPatientBridge, nameof(DoctorPatientBridge.Status), nameof(DoctorPatientBridge.DoctorID));

      return (doctorPatientBridge.Status).ToString();
    }

    public PatientDTO Subscription(SubscribeDto subscribeDto)
    {
      Patient patient = GetByID(subscribeDto.PatientId);

      DoctorPatientBridge doctorPatientBridge = new DoctorPatientBridge
      {
        DoctorID = subscribeDto.DoctorID,
        PatientID = subscribeDto.PatientId,
        Status = Status.Waiting,
      };
      _doctorPatirentRepository.Add(doctorPatientBridge);

      PatientDTO patientDTO = _mapper.Map<PatientDTO>(patient);

      return patientDTO;
    }
    public Patient GetByID(string id)
    {
      Patient patient = _context.Patient.Include(No=>No.NoEat).Include(docPat=>docPat.DoctorPatientBridge).ThenInclude(doc=>doc.Doctor).Include("ApplicationUser").Include(x=>x.CustomPlans).ThenInclude(x=>x.DaysCustomPlan).ThenInclude(x=>x.DayMealCustomPlanBridge).ThenInclude(x=>x.MealCustomPlan).FirstOrDefault(patient => patient.Id == id);

      return patient;
    }
    public PatientDTO GetPatientDTO(string PatientId)
    {
      Patient patient = GetByID(PatientId);

      PatientDTO patientDTO = _mapper.Map<PatientDTO>(patient);
      return patientDTO;
    }

    public void Delete(string id)
    {
      Patient patient = GetByID(id);
      patient.IsDeleted = true;
    }

    public IQueryable<Patient> Get(Expression<Func<Patient, bool>> expression)
    {
      IQueryable<Patient> patients = _context.Patient.Where(expression);
      return patients;
    }

    public IQueryable<Patient> GetAll()
    {
      IQueryable<Patient> patients = _context.Patient;
      return patients;
    }

    public void Update(Patient entity)
    {
      _context.Patient.Update(entity);
    }

    public void Update(Patient entity, params string[] properties)
    {
      var localEntity = _context.Patient.Local.Where(x => EqualityComparer<string>.Default.Equals(x.Id, entity.Id)).FirstOrDefault();

      EntityEntry entityEntry;

      if (localEntity is null)
      {
        entityEntry = _context.Patient.Entry(entity);
      }
      else
      {
        entityEntry =
            _context.ChangeTracker.Entries<Patient>()
            .Where(x => EqualityComparer<string>.Default.Equals(x.Entity.Id, entity.Id)).FirstOrDefault();
      }
      IEntityType entityType = _context.Model.FindEntityType(entity.GetType());
      foreach (var property in entityEntry.Properties)
      {
        IForeignKey foreignKey = entityType.FindForeignKeys(property.Metadata)
         .FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(ApplicationUser));


        if (foreignKey != null)
        {
          var applicationUserName = entityType.GetNavigations()
           .FirstOrDefault(n => n.TargetEntityType.ClrType == typeof(ApplicationUser))?
           .Name;

          var applicationUserProperty = entity.GetType().GetProperty(applicationUserName);
          var applicationUserValue = applicationUserProperty?.GetValue(entity);

          ApplicationUser referencedUser = _context.Set<ApplicationUser>().Find(entity.Id);
          EntityEntry userentityEntry = _context.Set<ApplicationUser>().Entry(referencedUser);

          foreach (var userproperty in userentityEntry.Properties)
          {
            if (properties.Contains(userproperty.Metadata.Name))
            {
              userproperty.CurrentValue = applicationUserValue.GetType().GetProperty(userproperty.Metadata.Name).GetValue(applicationUserValue);
              userproperty.IsModified = true;
            }
          }

        }
        else if (properties.Contains(property.Metadata.Name))
        {
          property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
          property.IsModified = true;
        }
      }
    }
    public string AddNote(PatientNotes patientNotes)
    {
      _context.PatientNotes.Add(patientNotes);
      return "Success";
    }

    public IQueryable<PatientNotes> GetNotes(GetPatientNotesDTO getPatientNotesDTO)
    {
      return _context.PatientNotes.Where(x => x.PatientId == getPatientNotesDTO.patientId)
      .Include(x => x.Patient);

    }



  }
}
