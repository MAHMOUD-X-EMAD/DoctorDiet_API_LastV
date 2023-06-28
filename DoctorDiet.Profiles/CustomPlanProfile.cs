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
  public class CustomPlanProfile:Profile
  {
        public CustomPlanProfile()
        {
            CreateMap<Plan, CustomPlan>()
                .ForMember(dst => dst.PatientId, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.DaysCustomPlan, opt => opt.Ignore())
                .ForMember(dst => dst.goal, opt => opt.Ignore())
            .ForMember(dst => dst.DoctorName, opt => opt.Ignore());



            CreateMap<Day, DayCustomPlan>()
            //.ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.CustomPlanId, opt => opt.Ignore());


            CreateMap<Meal, MealCustomPlan>()
              .ForMember(dst => dst.Id, opt => opt.Ignore());
        

            CreateMap<DayMealBridge, DayMealCustomPlanBridge>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.MealId, opt => opt.Ignore())
                .ForMember(dst => dst.DayId, opt => opt.Ignore());

            CreateMap<CustomPlan, ShowCustomPlanDto>();

            CreateMap<CustomPlan, CustomPlanDTO>()
    .ForMember(dest => dest.DaysCustomPlan, opt => opt.MapFrom(src => src.DaysCustomPlan));

            // Mapping configuration for CustomDay and CustomDayDTO
            CreateMap<DayMealCustomPlanBridge, CustomMealsDTO>() // Map DayMealCustomPlanBridge to CustomMealsDTO
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.MealCustomPlan.Image))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MealCustomPlan.Id))
            .ForMember(dest => dest.CustomPlanId, opt => opt.MapFrom(src => src.DayCustomPlan.CustomPlanId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.MealCustomPlan.Description))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.MealCustomPlan.Category))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.MealCustomPlan.Type));

            CreateMap<DayCustomPlan, CustomDayDTO>()
                .ForMember(dest => dest.CustomMeals, opt => opt.MapFrom(src => src.DayMealCustomPlanBridge));


            // Mapping configuration for CustomMeals and CustomMealsDTO
            CreateMap<MealCustomPlan, CustomMealsDTO>();

           

            CreateMap<UpdateMealDTO, MealCustomPlan>()
                .ForPath(src => src.Image, opt => opt.Ignore());


        }

     



        

    }
}
