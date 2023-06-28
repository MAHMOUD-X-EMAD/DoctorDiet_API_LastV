using DoctorDiet.Data;
using DoctorDiet.Models;
using DoctorDiet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Repositories
{
    public class PlanRepository:IPlanRepository
    {
        private readonly Context _context;
        private readonly IGenericRepository<Meal, int> _mealRepository;

        public PlanRepository(Context context, IGenericRepository<Meal, int> mealRepository)
        {

            _context = context;
            _mealRepository = mealRepository;
        }

        public IQueryable<Plan> GetAll()
        {
            return _context.Plans;
        }

        public IQueryable<Plan> Get(Expression<Func<Plan, bool>> expression)
        {
            return _context.Plans.Where(expression);
        }

        public Plan GetByID(int id)
        {
            return _context.Plans.FirstOrDefault(x => x.Id == id);
        }

        public Plan Add(Plan entity)
        {
            _context.Plans.Add(entity);
            return entity;
        }

        public void Update(Plan entity)
        {
            _context.Update(entity);
        }

        public void Update(Plan entity, params string[] properties)
        {
            var localEntity = _context.Plans.Local.FirstOrDefault(x => x.Id == entity.Id);

            EntityEntry entityEntry;

            if (localEntity == null)
            {
                entityEntry = _context.Plans.Entry(entity);
            }
            else
            {
                entityEntry = _context.ChangeTracker.Entries<Plan>()
                    .FirstOrDefault(x => x.Entity.Id == entity.Id);
            }

            foreach (var property in entityEntry.Properties)
            {
                if (properties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name)?.GetValue(entity);
                    property.IsModified = true;
                }
            }
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            if (entity != null)
            {
                _context.Plans.Remove(entity);
            }
        }

        public Meal UpdateMealPlan(Meal mealCustomPlan, params string[] properties)
        {

            _mealRepository.Update(mealCustomPlan, properties);

            return mealCustomPlan;
        }

    }
}
