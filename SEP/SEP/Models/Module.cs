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

        [Required(ErrorMessage = "* Module Code is required")]
        [Display(Name = "Module Code")]
        public string ModuleId { get; set; }
        [Display(Name = "Module Name")]
        public string Description { get; set; }
        [Display(Name = "Max Students")]
        public int MaxCount { get; set; }
        public DateTime DeadLine { get; set; }
        public int Year { get; set; }
        public int semester { get; set; }
        [Display(Name = "Lecturer Incharge")]
        public String LecturerIncharge { get; set; }

        [Display(Name = "Maximum Lec In Panel")]
        public int MaxLecPanels  { get; set; }


        public static implicit operator Module(Group v)
        {
            throw new NotImplementedException();
        }
    }
}