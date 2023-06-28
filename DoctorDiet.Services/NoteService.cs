using AutoMapper;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using DoctorDiet.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Services
{
  public class NoteService
  {
    IGenericRepository<DoctorNotes, int> _notesRepository;
    IUnitOfWork _unitOfWork;
    IMapper _mapper;
    public NoteService(IGenericRepository<DoctorNotes, int> notesRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
      _notesRepository = notesRepository;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    public DoctorNotes AddNote(NoteCreateDto noteCreateDto)
    {
      DoctorNotes note = _mapper.Map<DoctorNotes>(noteCreateDto);
      _notesRepository.Add(note);
      _unitOfWork.SaveChanges();

      return note;

    }

    public IEnumerable<DoctorNotes> GetAllNotesByDocID(string docID)
    {
      IEnumerable<DoctorNotes> notes = _notesRepository.Get(note => note.DoctorId == docID).ToList();

      return notes;
    }

    public DoctorNotes GetNoteByID(int noteID)
    {
      DoctorNotes note = _notesRepository.GetByID(noteID);

      return note;
    }

    public IEnumerable<DoctorNotes> GetAllNotesByDayID(int dayID)
    {
      IEnumerable<DoctorNotes> notes = _notesRepository.Get(note => note.DayCustomPlanId == dayID).ToList();

      return notes;
    }

    public void updateNote(int noteID, UpdateNoteDto updateNoteDto, params string[] updatedProp)
    {

      DoctorNotes note = GetNoteByID(noteID);
      note.Text = updateNoteDto.Text;
      _notesRepository.Update(note, updatedProp);
      _unitOfWork.SaveChanges();
    }

    public void DeleteNote(int noteID)
    {
      _notesRepository.Delete(noteID);
      _unitOfWork.SaveChanges();

    }
  }
}
