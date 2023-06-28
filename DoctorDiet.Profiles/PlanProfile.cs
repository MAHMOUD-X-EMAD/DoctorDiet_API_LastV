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
  public class PlanProfile : Profile
  {
    public PlanProfile()
    {
      CreateMap<AddPlanDTO, Plan>()
       .ForMember(dst => dst.Allergics, opt => opt.Ignore())
       .ForMember(dst => dst.Days, opt => opt.Ignore());

            CreateMap<Day, DayDTO>()
               .ForMember(dst => dst.Meals, opt => opt.MapFrom(src => src.DayMeal));


            CreateMap<DayMealBridge, MealDTO>()
           .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Meal.Image))
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Meal.Description))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Meal.Category))
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Meal.Type));


        }
  }
}
