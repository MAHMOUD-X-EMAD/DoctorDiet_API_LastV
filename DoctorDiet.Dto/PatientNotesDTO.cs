using DoctorDiet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class PatientNotesDTO
    {
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public string PatientId { get; set; }
    public int DayCustomPlanId { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
  }
}
