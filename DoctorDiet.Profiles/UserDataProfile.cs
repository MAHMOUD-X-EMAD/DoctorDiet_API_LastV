using AutoMapper;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Profiles
{
    public class UserDataProfile:Profile
    {
        public UserDataProfile()
        {
            CreateMap<Patient, UserDataDTO>()
                .ForPath(dst => dst.phoneNumber,
                opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))

                .ForPath(dst => dst.email,
                opt => opt.MapFrom(src => src.ApplicationUser.Email))

                .ForPath(dst => dst.UserName,
                opt => opt.MapFrom(src => src.ApplicationUser.UserName))

                .ForPath(dst => dst.ProfileImage,
                opt => opt.MapFrom(src => src.ApplicationUser.ProfileImage));





        }
    }
}
