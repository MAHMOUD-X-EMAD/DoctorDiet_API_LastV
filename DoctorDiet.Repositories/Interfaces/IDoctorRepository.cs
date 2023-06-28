using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repositories.Interfaces
{
    public interface IDoctorRepository 
    {
    IQueryable<DoctorNotes> GetNotes(GetDoctorNotesDTO getdoctorNotesDTO);
    string AddNote(DoctorNotes Notes);
    void Update(Doctor entity, params string[] properties);
    void Update(Doctor entity);
    IQueryable<Doctor> GetAll();
    IQueryable<Doctor> Get(Expression<Func<Doctor, bool>> expression);
    void Delete(string id);
    Doctor GetPatientDTO(string doctorId);
    Doctor GetByID(string id);
    Doctor Add(Doctor entity);
    }
}
