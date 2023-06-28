using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorDiet.Models.Interface;

namespace DoctorDiet.Models
{
    public class Doctor:IBaseModel<string>
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string FullName { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        public List<ContactInfo>? ContactInfo { get; set; }
    public List<PatientNotes>? PatientNotes { get; set; }

    public virtual List<Plan>? Plan { get; set; }

      public virtual List<DoctorPatientBridge>? DoctorPatientBridge { get; set; }

        public string Specialization { get; set; }
        public string Location { get; set; }


    }
}
