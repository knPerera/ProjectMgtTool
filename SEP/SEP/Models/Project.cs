using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class Project
    {
        [Key]
        public string ProjectId { get; set; }
        public string ModuleId{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public string PreferedTechnologies { get; set; }
    }
}