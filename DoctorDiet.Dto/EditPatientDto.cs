using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
  public class EditPatientDto
  {
    public string Id { get; set; }
    public string? Gender { get; set; }
    public string? FullName { get; set; }
    public string? Diseases { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public double? Weight { get; set; }
   public double? Height { get; set; }
   public string? ActivityRates { get; set; }

  }
}
