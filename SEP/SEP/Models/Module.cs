using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Module
    {
        [Key]
        public string ModuleId { get; set; }
        public string Description { get; set; }
        public int MaxCount { get; set; }
        public DateTime DeadLine { get; set; }
    }
}