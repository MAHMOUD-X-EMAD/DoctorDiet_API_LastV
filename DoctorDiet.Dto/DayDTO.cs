using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class DayDTO
    {
      public int Id { get; set; } 
      public  List<MealDTO> Meals { get; set; }
    }
}
