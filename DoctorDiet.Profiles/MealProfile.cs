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
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<MealDTO, Meal>()
           .ForMember(dst => dst.Image, opt => opt.Ignore());
            CreateMap<Meal, MealDTO>();
      

            CreateMap<UpdateMealDTO, Meal>()
                .ForPath(src => src.Image, opt => opt.Ignore());
        }
    }
}
