using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class AllocateSupervisors
    {
        [Key]
        public int id { get; set; }
        public string groupid { get; set; }
        public string supervisor { get; set; }
    }
}