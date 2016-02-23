using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact No.")]
        public int ContactNo { get; set; }

    }
}