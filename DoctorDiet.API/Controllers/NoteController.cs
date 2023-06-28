using AutoMapper;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.UnitOfWork;
using DoctorDiet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorDiet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        NoteService _NoteService;
        IUnitOfWork _unitOfWork;
       
        public NoteController(NoteService NoteService, IUnitOfWork unitOfWork) 
        {
            _NoteService= NoteService;
            _unitOfWork= unitOfWork;
        }

    [HttpPost("AddNote")]
    public IActionResult AddNote(NoteCreateDto noteCreateDto)
    {
      if (ModelState.IsValid)
      {
        DoctorNotes note = _NoteService.AddNote(noteCreateDto);
        _unitOfWork.CommitChanges();

        return Ok(note);
      }
      else
      {
        return BadRequest(ModelState);
      }

    }

    [HttpGet("GetAllNotesByDocID{doctorID}")]
    public IActionResult GetAllNotesByDocID(string doctorID)
    {
      if (ModelState.IsValid)
      {
        IEnumerable<DoctorNotes> notes = _NoteService.GetAllNotesByDocID(doctorID);

        return Ok(notes);
      }
      else
      {
        return BadRequest(ModelState);
      }

    }


    [HttpGet("GetAllNotesByDayID{dayID}")]
    public IActionResult GetAllNotesByDayID(int dayID)
    {
      if (ModelState.IsValid)
      {
        IEnumerable<DoctorNotes> notes = _NoteService.GetAllNotesByDayID(dayID);

        return Ok(notes);
      }
      else
      {
        return BadRequest(ModelState);
      }

    }

    [HttpGet("GetNoteByID{noteID}")]
    public IActionResult GetNoteByID(int noteID)
    {
      if (ModelState.IsValid)
      {
        DoctorNotes note = _NoteService.GetNoteByID(noteID);
        return Ok(note);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    [HttpPut("UpdateNote{noteID}")]
    public IActionResult UpdateNote(int noteID, UpdateNoteDto UpdateNoteDto, params string[] updatedProp)
    {
      if (ModelState.IsValid)
      {

        _NoteService.updateNote(noteID, UpdateNoteDto, updatedProp);
        _unitOfWork.CommitChanges();

        return Ok(" Successfully Updated");
      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    [HttpDelete("DeleteNote{noteID}")]
    public IActionResult DeleteNote(int noteID)
    {
      if (ModelState.IsValid)
      {
        _NoteService.DeleteNote(noteID);
        _unitOfWork.CommitChanges();

        return Ok("Successfully Deleted!");
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
  }
}
