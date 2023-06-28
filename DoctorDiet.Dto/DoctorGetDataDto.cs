using DoctorDiet.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorDiet.Dto
{
    public class DoctorGetDataDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public byte[] ProfileImage { get; set; }

        public string Specialization { get; set; }
        public string Location { get; set; }
        public List<ContactInfoDTO> contactInfo { get; set; }

    }
}
