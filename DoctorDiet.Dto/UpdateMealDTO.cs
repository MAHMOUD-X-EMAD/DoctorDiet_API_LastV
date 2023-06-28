using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public  class UpdateMealDTO
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
    }
}
