using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class PatientNotes:IBaseModel<int>
    {
    public int Id { get; set; }

    [ForeignKey("Patient")]
    public string PatientId { get; set; }

    [ForeignKey("Doctor")]
    public string DoctorId { get; set; }
    [ForeignKey("DayCustomPlan")]
    public int DayCustomPlanId { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public virtual DayCustomPlan DayCustomPlan { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }

    [DefaultValue("false")]
    public bool IsDeleted { get; set; }
  }
}
