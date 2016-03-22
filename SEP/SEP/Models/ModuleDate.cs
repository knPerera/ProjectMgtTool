using System;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models
{
    public class ModuleDate
    {
        [Key]

        [Required(ErrorMessage = "* Module Code is required")]
        [Display(Name = "Module Code")]
        public string ModuleId { get; set; }
        [Display(Name = "Module Starting Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Module Ending Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Group Finalizing Date")]
        public DateTime GroupFinalizeDate { get; set; }

    }
}