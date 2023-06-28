using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Models;
using DoctorDiet.Models.Interface;
using DoctorDiet.Data.Extentions;

namespace DoctorDiet.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {


        public Context() { }
        public Context(DbContextOptions dbContextOptions) : base(dbContextOptions) { }


        public DbSet<Admin> Admin { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Day> Day { get; set; }
    public DbSet<AllergicsPlan> AllergicsPlans { get; set; }

    public DbSet<DayCustomPlan> DayCustomPlans { get; set; }
    public DbSet<DayMealCustomPlanBridge> DayMealCustomPlanBridges { get; set; }

    public DbSet<DayMealBridge> DayMealBridge{ get; set; }
    public DbSet<MealCustomPlan> MealCustomPlans { get; set; }
    public DbSet<Meal> Meal { get; set; }
        public DbSet<NoEat> NoEat { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<CustomPlan> CustomPlans { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<DoctorPatientBridge> doctorPatientBridges { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ActivityRate> ActivityRate { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Goal> Goals { get; set; }
    public DbSet<DoctorNotes> DoctorNotes { get; set; }
    public DbSet<PatientNotes> PatientNotes { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyGlobalFilter<IBaseModel<int>>(x => !x.IsDeleted);

            modelBuilder.ApplyGlobalFilter<IBaseModel<string>>(x => !x.IsDeleted);

        }

    }
}
