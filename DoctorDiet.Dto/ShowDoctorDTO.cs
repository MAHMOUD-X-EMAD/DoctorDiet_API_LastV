using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class ShowDoctorDTO
    {
        public string Id { get; set; }

        public string FullName { get; set; }
        public byte[] ProfileImage { get; set; }

    }
}
