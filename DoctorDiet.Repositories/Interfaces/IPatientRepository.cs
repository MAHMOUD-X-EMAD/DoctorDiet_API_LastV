using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repositories.Interfaces
{
    public interface IPatientRepository
    {
       
            string Reject(SubscribeDto subscribeDto);
            string AddNote(PatientNotes patientNotes);

    IQueryable<PatientNotes> GetNotes(GetPatientNotesDTO getPatientNotesDTO);
            PatientDTO Subscription(SubscribeDto subscribeDto);
    Patient GetByID(string id);
    PatientDTO GetPatientDTO(string PatientId);
  }
}
