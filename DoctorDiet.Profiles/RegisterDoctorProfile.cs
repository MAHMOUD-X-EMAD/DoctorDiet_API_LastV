using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.DTO;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakiny.Profiles
{
    public class RegisterDoctorProfile:Profile
    {
        public RegisterDoctorProfile()
        {

            CreateMap<RegisterDoctorDto, ApplicationUser>()
            .ForMember(dst => dst.ProfileImage, opt => opt.Ignore());

           CreateMap<RegisterDoctorDto, Doctor>()
            .ForMember(dst => dst.ContactInfo, opt => opt.Ignore());


           CreateMap<Doctor, DoctorDataDTO>();
            CreateMap<DoctorDataDTO, Doctor>()
                      .ForPath(dest => dest.ApplicationUser.UserName, opt => opt.MapFrom(src => src.UserName))
                      .ForPath(dest => dest.ApplicationUser.Email, opt => opt.MapFrom(src => src.Email))
                      .ForPath(dst => dst.ApplicationUser.ProfileImage, opt => opt.Ignore());



            CreateMap<DoctorPatientBridge,DoctorPatientBridgeDTO> ()
                .ForMember(dst=>dst.FullName,opt=>opt.MapFrom(src=>src.Doctor.FullName));


            CreateMap<ContactInfoDTO, ContactInfo>();   

            CreateMap<DoctorGetDataDto, Doctor>()
         .ForPath(dest => dest.ApplicationUser.UserName, opt => opt.MapFrom(src => src.UserName))
         .ForPath(dest => dest.ApplicationUser.Email, opt => opt.MapFrom(src => src.Email))
         .ForPath(dest => dest.ApplicationUser.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
         .ReverseMap();


            CreateMap<Doctor, ShowDoctorDTO>()
         .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ApplicationUser.ProfileImage));

        }
    }
}
