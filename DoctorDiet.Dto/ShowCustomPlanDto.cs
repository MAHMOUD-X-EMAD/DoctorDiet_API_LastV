using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class ShowCustomPlanDto
    {
        public int Id { get; set; }
        public string goal { get; set; }
        public string DoctorName { get; set; }
        public int Duration { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
