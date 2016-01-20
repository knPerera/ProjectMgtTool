using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Lecturer
    {
        [Key]
        public string LecturerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ContactNo { get; set; }
        public string Module { get; set; }
        public string Qualification { get; set; }
        public string Avatar { get; set; }
        public string password { get; set; }



    }
}