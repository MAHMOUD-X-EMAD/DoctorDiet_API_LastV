using DoctorDiet.Models.Interface;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorDiet.Models
{
    public class DoctorNotes : IBaseModel<int>
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        [ForeignKey("DayCustomPlan")]
        public int DayCustomPlanId { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public string Text { get; set; }

        public DateTime Date { get; set; }
        public virtual DayCustomPlan DayCustomPlan { get; set; }
 
        public virtual Doctor Doctor { get; set; }

        public virtual Patient Patient { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}