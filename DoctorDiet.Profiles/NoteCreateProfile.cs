using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
    public class NoteCreateProfile:Profile
    {
        public NoteCreateProfile()
        {
      CreateMap<NoteCreateDto, DoctorNotes>();
      CreateMap<UpdateNoteDto, DoctorNotes>();

      CreateMap<DoctorNotesDTO, DoctorNotes>().ReverseMap();


      CreateMap<PatientNotes, GetPatientNoteData>()
         .ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.Patient.FullName));

      CreateMap<DoctorNotes, GetDoctorNoteData>();
    }
    }
}
