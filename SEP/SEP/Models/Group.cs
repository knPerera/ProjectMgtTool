using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Group
    {
        [Key]
        public string GroupID { get; set; }
        public string ProjectID { get; set; }
        public string Supervisor { get; set; }

        


    }
}