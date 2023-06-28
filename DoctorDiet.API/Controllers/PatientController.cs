using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
        {
            PatientService _patientService;
            IUnitOfWork _unitOfWork;

            public PatientController(PatientService patientService, IUnitOfWork unitOfWork)
            {
               _patientService = patientService;
               _unitOfWork = unitOfWork;
            }

        [HttpGet("GetAllPatients")]
        public IActionResult GetAllPatients()
        {

            return Ok(_patientService.GetAllPatients());

        }


        [HttpGet("patientid")]
        public IActionResult GetPatientById(string patientid)
        {
            var patient = _patientService.GetPatientData(patientid);

            return Ok(patient);

        }
        [HttpGet("GetAllDaysOfSpecificCustomPlan{PatientId}")]
        public IActionResult GetAllDaysOfSpecificCustomPlan(string PatientId)
        {
            if (ModelState.IsValid)
            {
                List<List<DayCustomPlan>> DaysWithMeals = _patientService.GetEveryDayWithMealsOfDay(PatientId);
                return Ok(DaysWithMeals);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpGet("GetPatientHistory/{PatientId}")]
        public IActionResult GetPatientHistory(string PatientId)
        {
            if (ModelState.IsValid)
            {
                List<ShowCustomPlanDto> customsPlans = _patientService.GetPatientHistory(PatientId);
                return Ok(customsPlans);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    [HttpPost("AddPatientNote")]
    public IActionResult AddNote(PatientNotesDTO patientNotesDto)
    {
      _patientService.AddNote(patientNotesDto);
      _unitOfWork.CommitChanges();
      return NoContent();
    }

    [HttpGet("GetPatientsNotes")]
    public IActionResult GetNote(string patientId, int dayId)
    {
      GetPatientNotesDTO getPatientNotesDTO = new GetPatientNotesDTO();
      getPatientNotesDTO.patientId = patientId;
      getPatientNotesDTO.dayId = dayId;
      List<GetPatientNoteData> PatientNotes = _patientService.GetPateintNotes(getPatientNotesDTO);

      return Ok(PatientNotes);
    }

    [HttpPut("ConfirmAccount")]
        public IActionResult ConfirmAccount(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
                string Status = _patientService.Confirm(subscribeDto);
                if (Status != "")
                {
                    _unitOfWork.CommitChanges();

                    return Ok(new
                    {
                        msg=Status
                    });
                }
                else
                {
                    return Ok(new
                    {
                        msg="NotFound"
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPut("RejectAccount")]
        public IActionResult RejectAccount(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
                string Status = _patientService.Reject(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(new
                {
                    msg=Status
                });
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPost("Subscribtion")]
        public IActionResult Subscribtion(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
               PatientDTO patientDTO= _patientService.Subscription(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(patientDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }


        [HttpGet("patientDataDTO/{patientid}")]
        public IActionResult GetPatientDtoById(string patientid)
        {
            UserDataDTO patient = _patientService.GetPatientDataDTO(patientid);

            return Ok(patient);

        }

    [HttpGet("patientDTO/{patientid}")]
    public IActionResult GetPatientDto(string patientid)
   {
      PatientDTO patient = _patientService.GetPatientDTO(patientid);

      return Ok(patient);

    }

    [HttpGet("GetPatientsByDoctorIdWithStatusConfirmed")]
        public IActionResult GetPatientsByDoctorIdWithStatusConfirmed(string Doctorid)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<PatientDTO> patients = _patientService.GetPatientsByDoctorIdWithStatusConfirmed(Doctorid);
                return Ok(patients);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetPatientsByDoctorIdWithStatusWaiting")]
        public IActionResult GetPatientsByDoctorIdWithStatusWaiting(string Doctorid)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<PatientDTO> patients = _patientService.GetPatientsByDoctorIdWithStatusWaiting(Doctorid);
                return Ok(patients);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("EditPatientData")]
        public IActionResult EditPatientData([FromForm] EditPatientDto patientData, [FromForm] params string[] properties)
        {
            List<string> Properties = properties[0].Split(',').ToList();
            string[] propertiesArray = Properties.ToArray();
            if (ModelState.IsValid)
            {
                _patientService.EditPatientData(patientData, propertiesArray);
                _unitOfWork.CommitChanges();
                return Ok(new
                {
                    msg="done"
                });
            }


            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpGet("GetIFPatientInSubscription/{patientid}")]
        public IActionResult GetIFPatientInSubscription(string patientid)
        {
            string Status = _patientService.GetStatus(patientid);

            return Ok(new
            {
                msg = Status
            });

        }

        [HttpPut("CanceledSubscription")]
        public IActionResult CanceledSubscription(SubscribeDto subscribeDto)
        {
            if (ModelState.IsValid)
            {
                string Status = _patientService.CancelStatus(subscribeDto);
                _unitOfWork.CommitChanges();

                return Ok(new
                {
                    msg = Status
                });
            }
            else
            {
                return BadRequest(ModelState);
            }


        }



    }
}
