using DoctorDiet.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class MealDTO
    {
       public int Id { get; set; }  
       public string Description { get; set; }
       public Models.Type Type { get; set; }

       public byte[] Image { get; set; }
       public  Category Category { get; set; }

        public int DayId { get; set; }    


   

  }
}
