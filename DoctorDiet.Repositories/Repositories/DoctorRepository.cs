using AutoMapper;
using DoctorDiet.Data;
using DoctorDiet.Dto;
using DoctorDiet.Models;
using DoctorDiet.Repositories.Interfaces;
using DoctorDiet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Repositories.Repositories
{
  public class DoctorRepository : IDoctorRepository
  {
    Context _context;
    ICustomPlanRepository _CustomPlanRepository;
    IMapper _mapper;
    IGenericRepository<DoctorPatientBridge, int> _doctorPatirentRepository;
    public DoctorRepository(Context context, ICustomPlanRepository CustomPlanRepository,
    IGenericRepository<DoctorPatientBridge, int> doctorPatirentRepository,
IMapper mapper)
    {
      _context = context;
      _CustomPlanRepository = CustomPlanRepository;
      _mapper = mapper;
      _doctorPatirentRepository = doctorPatirentRepository;
    }


    public Doctor Add(Doctor entity)
    {
      _context.Add(entity);
      return entity;
    }



    public Doctor GetByID(string id)
    {
      Doctor patient = _context.Doctors.Include(docPat => docPat.DoctorPatientBridge).Include("ApplicationUser").Include(x => x.Plan).ThenInclude(x => x.Days).ThenInclude(x => x.DayMeal).ThenInclude(x => x.Meal).FirstOrDefault(patient => patient.Id == id);

      return patient;
    }
    public Doctor GetPatientDTO(string doctorId)
    {
      Doctor doctor = GetByID(doctorId);


      return doctor;
    }

    public void Delete(string id)
    {
      Doctor doctor = GetByID(id);
      doctor.IsDeleted = true;
    }

    public IQueryable<Doctor> Get(Expression<Func<Doctor, bool>> expression)
    {
      IQueryable<Doctor> doctors = _context.Doctors.Where(expression);
      return doctors;
    }

    public IQueryable<Doctor> GetAll()
    {
      IQueryable<Doctor> doctors = _context.Doctors;
      return doctors;
    }

    public void Update(Doctor entity)
    {
      _context.Doctors.Update(entity);
    }

    public void Update(Doctor entity, params string[] properties)
    {
      var localEntity = _context.Patient.Local.Where(x => EqualityComparer<string>.Default.Equals(x.Id, entity.Id)).FirstOrDefault();

      EntityEntry entityEntry;

      if (localEntity is null)
      {
        entityEntry = _context.Doctors.Entry(entity);
      }
      else
      {
        entityEntry =
            _context.ChangeTracker.Entries<Doctor>()
            .Where(x => EqualityComparer<string>.Default.Equals(x.Entity.Id, entity.Id)).FirstOrDefault();
      }
      IEntityType entityType = _context.Model.FindEntityType(entity.GetType());
      foreach (var property in entityEntry.Properties)
      {
        IForeignKey foreignKey = entityType.FindForeignKeys(property.Metadata)
         .FirstOrDefault(fk => fk.PrincipalEntityType.ClrType == typeof(ApplicationUser));


        if (foreignKey != null)
        {
          var applicationUserName = entityType.GetNavigations()
           .FirstOrDefault(n => n.TargetEntityType.ClrType == typeof(ApplicationUser))?
           .Name;

          var applicationUserProperty = entity.GetType().GetProperty(applicationUserName);
          var applicationUserValue = applicationUserProperty?.GetValue(entity);

          ApplicationUser referencedUser = _context.Set<ApplicationUser>().Find(entity.Id);
          EntityEntry userentityEntry = _context.Set<ApplicationUser>().Entry(referencedUser);

          foreach (var userproperty in userentityEntry.Properties)
          {
            if (properties.Contains(userproperty.Metadata.Name))
            {
              userproperty.CurrentValue = applicationUserValue.GetType().GetProperty(userproperty.Metadata.Name).GetValue(applicationUserValue);
              userproperty.IsModified = true;
            }
          }

        }
        else if (properties.Contains(property.Metadata.Name))
        {
          property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
          property.IsModified = true;
        }
      }
    }
    public string AddNote(DoctorNotes Notes)
    {
      _context.DoctorNotes.Add(Notes);
      return "Success";
    }

    public IQueryable<DoctorNotes> GetNotes(GetDoctorNotesDTO getdoctorNotesDTO)
    {

      IQueryable<DoctorNotes> DoctorNotes = _context.DoctorNotes
      .Where(x => x.DoctorId == getdoctorNotesDTO.DoctorId && x.PatientId == getdoctorNotesDTO.PatientId);

      return DoctorNotes;
    }

  }
}
