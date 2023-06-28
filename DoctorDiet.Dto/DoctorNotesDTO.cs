using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class DoctorNotesDTO
    {
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public string PatientId { get; set; }
    public int DayCustomPlanId { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
  }
}
