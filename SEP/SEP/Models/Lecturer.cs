using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    
    public class Lecturer 
    {
        [Key]
        [Required(ErrorMessage ="The Lecture ID Is A Required Field")]
        public string LecturerId { get; set; }
        [Required(ErrorMessage = "The Name Is A Required Field")]
        [DataType(DataType.Text,ErrorMessage ="Don't include numarical values or symbols in u're name")]
        [StringLength(40,ErrorMessage ="This field required a smaller name than this")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "The Email Is A Require Field")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Contact No Is A Require Field")]
        [Phone(ErrorMessage ="U have Not Entered The Phone Correctly")]
        public int ContactNo { get; set; }
        public string Module { get; set; }
        public string Qualification { get; set; }
        [DataType(DataType.ImageUrl,ErrorMessage ="Please Select A Valid Image")]
        [Required(ErrorMessage = "The Avatar Is A Require Field")]
        public string Avatar { get; set; }

        public static implicit operator Lecturer(AllocatedLecturers v)
        {
            throw new NotImplementedException();
        }
    }
}