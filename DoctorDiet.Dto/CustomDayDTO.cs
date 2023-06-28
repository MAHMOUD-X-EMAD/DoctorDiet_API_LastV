using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class CustomDayDTO
    {
        public int Id { get; set; }
        public List<CustomMealsDTO> CustomMeals { get; set; }
    }
}
