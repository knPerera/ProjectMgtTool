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
        public int ProjectId { get; set; }
        [Display(Name = "Module Name")]
        public string ModuleId { get; set; }
        [Display(Name = "Project Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public string PreferedTechnologies { get; set; }
    }
}