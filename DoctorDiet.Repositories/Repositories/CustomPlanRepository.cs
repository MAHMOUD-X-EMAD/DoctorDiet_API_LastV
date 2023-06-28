using AutoMapper;
using DoctorDiet.Data;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Repositories
{
  public class CustomPlanRepository : ICustomPlanRepository
  {
    Context _context;
    IMapper _mapper;
    IGenericRepository<DayCustomPlan, int> _dayCustomPlanRepository;
    IGenericRepository<MealCustomPlan, int> _mealCustomPlanRepository;
    IGenericRepository<DayMealCustomPlanBridge, int> _DayMealCustomPlanBridgeRepository;

    public CustomPlanRepository(Context context, IMapper mapper,
      IGenericRepository<DayCustomPlan, int> dayCustomPlanRepository,
      IGenericRepository<MealCustomPlan, int> mealCustomPlanRepository,
      IGenericRepository<DayMealCustomPlanBridge, int> DayMealCustomPlanBridgeRepository)
    {
      _context = context;
      _mapper = mapper;
      _dayCustomPlanRepository = dayCustomPlanRepository;
      _mealCustomPlanRepository = mealCustomPlanRepository;
      _DayMealCustomPlanBridgeRepository = DayMealCustomPlanBridgeRepository;
    }

    public IQueryable<CustomPlan> GetAll()
    {
      return _context.CustomPlans;
    }

    public IQueryable<CustomPlan> Get(Expression<Func<CustomPlan, bool>> expression)
    {
      return _context.CustomPlans.Where(expression);
    }

    public CustomPlan GetByID(int id)
    {
      return _context.CustomPlans.FirstOrDefault(x => EqualityComparer<int>.Default.Equals(x.Id, id));
    }

    public CustomPlan Add(CustomPlan entity)
    {
      /*Plan plan = _context.Plans.FirstOrDefault(p => p.CaloriesFrom < );*/

      _context.CustomPlans.Add(entity);

      return entity;
    }

    public void Update(CustomPlan entity)
    {
      _context.Update(entity);
    }

    public void Update(CustomPlan entity, params string[] properties)
    {
      var localEntity = _context.Plans.Local.Where(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)).FirstOrDefault();

      EntityEntry entityEntry;

      if (localEntity is null)
      {
        entityEntry = _context.CustomPlans.Entry(entity);
      }
      else
      {
        entityEntry =
            _context.ChangeTracker.Entries<CustomPlan>()
            .Where(x => EqualityComparer<int>.Default.Equals(x.Entity.Id, entity.Id)).FirstOrDefault();
      }

      foreach (var property in entityEntry.Properties)
      {
        if (properties.Contains(property.Metadata.Name))
        {
          property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
          property.IsModified = true;
        }
      }

    }

    public void Delete(int id)
    {
      var entity = GetByID(id);
      entity.IsDeleted = true;
    }

    public List<DayCustomPlan> GetDayList(int planId)
    {
      var days = _context.CustomPlans.FirstOrDefault(x => x.Id == planId)?.DaysCustomPlan;

      return days;
    }

        public CustomPlan AddCustomPlan(CustomPlan entity)
        {
            _context.CustomPlans.Add(entity);

            return entity;
        }

        public DayCustomPlan AddDayCustomPlan(DayCustomPlan dayCustomPlan)
    {
            DayCustomPlan DayCustomPlan = _dayCustomPlanRepository.Add(dayCustomPlan);


      return DayCustomPlan;

    }
    public MealCustomPlan AddMealCustomPlan(MealCustomPlan mealCustomPlan)
    {
      MealCustomPlan MealCustomPlan = _mealCustomPlanRepository.Add(mealCustomPlan);


      return MealCustomPlan;
    }
    public DayMealCustomPlanBridge AddDayMealCustomPlanBridge(DayMealCustomPlanBridge DayMealCustomPlan)
    {
      DayMealCustomPlanBridge DayMealCustomPlanBridge = _DayMealCustomPlanBridgeRepository.Add(DayMealCustomPlan);


      return DayMealCustomPlanBridge;
    }


     public CustomDayDTO GetDayCustomPlan(int id)
        {
            CustomPlan myPlan = Get(cusID => cusID.Id == id)
            .Include(d => d.DaysCustomPlan)
            .ThenInclude(bridge => bridge.DayMealCustomPlanBridge)
            .ThenInclude(m=>m.MealCustomPlan).FirstOrDefault();

            DateTime currentDate = DateTime.Today;
            int dayCount = (currentDate - myPlan.DateFrom).Days;

            if (dayCount >= 0 && dayCount < myPlan.Duration)
            {
                DayCustomPlan dayCustomPlan = myPlan.DaysCustomPlan[dayCount];
                CustomDayDTO customDayDTO = _mapper.Map<CustomDayDTO>(dayCustomPlan);

                return customDayDTO;
            }

            return null;
     }


        public MealCustomPlan UpdateMealCustomPlan(MealCustomPlan mealCustomPlan, params string[] properties)
        {

            _mealCustomPlanRepository.Update(mealCustomPlan, properties);

            return mealCustomPlan;
        }






    }
}
