using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {

        DoctorService _doctorService;
        IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public DoctorController(DoctorService doctorService, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _doctorService = doctorService;
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [HttpGet("doctorid")]
        public IActionResult GetDoctorById(string doctorid)
        {
            DoctorGetDataDto doctor = _doctorService.GetDoctorData(doctorid);

            return Ok(doctor);

        }



    [HttpPost("GetDoctorNotes")]
    public IActionResult GetDoctorNotes(GetDoctorNotesDTO getDoctorNotesDTO)
    {
      List<GetDoctorNoteData> DoctorNotes = _doctorService.GetDoctorNotes(getDoctorNotesDTO);


      return Ok(DoctorNotes);
    }

    [HttpPost("AddDoctorNote")]
    public IActionResult AddNote(DoctorNotesDTO doctorNotesDto)
    {
      _doctorService.AddNote(doctorNotesDto);
      unitOfWork.CommitChanges();
      return NoContent();
    }


    [HttpPut("EditDoctorData")]
    public IActionResult EditDoctorData([FromForm] DoctorDataDTO doctorData, [FromForm] params string[] properties)
    {
      if (ModelState.IsValid)
      {
        _doctorService.EditDoctorData(doctorData, properties);
        unitOfWork.CommitChanges();
        return Ok("sucess");
      }


      else
      {
        return BadRequest(ModelState);
      }

    }
    [HttpGet("GetAllDoctors")]
        public IActionResult GetAllDoctor()
        {
            if (ModelState.IsValid)
            {
                List<ShowDoctorDTO> doctors = _doctorService.GetListOfDoctors();
                return Ok(doctors);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpPost("ChangePassowrd")]
        public async Task<IActionResult> ChangePassowrd(DoctorNewPasswordDTO newPassowrd)
        {

            if (ModelState.IsValid)
            {
                Doctor doctor = _doctorService.GetById(newPassowrd.DoctorId);

                if (doctor != null)
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(doctor.ApplicationUser, newPassowrd.Password);

                    var token = await _userManager.GeneratePasswordResetTokenAsync(doctor.ApplicationUser);
                    IdentityResult result = await _userManager.ResetPasswordAsync(doctor.ApplicationUser, token, newPasswordHash);
                    RegisterDto registerDto = new RegisterDto();
                    unitOfWork.CommitChanges();

                    if (result.Succeeded)
                    {
                        registerDto.Message = "Success";
                        return Ok(registerDto);
                    }
                    else
                        registerDto.Message = "Failed";
                    return BadRequest(registerDto);
                }

            }
            return BadRequest(ModelState);

        }

    }


}
