using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repository.Interfaces
{
    public interface IPlanRepository : IGenericRepository<Plan,int>
    {
        public Meal UpdateMealPlan(Meal mealCustomPlan, params string[] properties);

    }
}
