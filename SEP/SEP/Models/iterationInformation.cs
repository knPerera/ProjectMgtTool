using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class iterationInformation
    {
        [Key]
        public int iterationId { get; set; }

        [Required(ErrorMessage = " please enter a name for the iteration")]
        [Display(Name = "Iteration Name")]
        public string iteration { get; set; }

        [Required(ErrorMessage = "Please enter the total mark given for the demonstation")]
        [Display(Name = "Total Mark giving")]
        public int totalMark { get; set; }
    }
}