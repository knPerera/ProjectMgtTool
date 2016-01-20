using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Student
    {
        [Key]
        public string RegistrationNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ContactNo { get; set; }
        public double CGPA { get; set; }
        public string Avatar { get; set; }
        public string CV { get; set; }
        public string Password { get; set; }

    }
}