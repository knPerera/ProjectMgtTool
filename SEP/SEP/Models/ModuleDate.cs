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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Module Ending Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Group Finalizing Date")]
        [DataType(DataType.Date)]
        public DateTime GroupFinalizeDate { get; set; }

    }
}