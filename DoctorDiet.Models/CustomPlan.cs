using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class CustomPlan: IBaseModel<int>
    {
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set;} 
        public int CaloriesFrom { get; set; }
        public int CaloriesTo { get; set; }
        public int Duration { get; set; }
        public virtual List<DayCustomPlan> DaysCustomPlan { get; set; }
        public virtual Patient Patient { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        public string goal { get; set; }
        public string DoctorName { get; set; }


    }
}
