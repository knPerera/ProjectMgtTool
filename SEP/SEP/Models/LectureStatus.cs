using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class LectureStatus
    {
        [Key]
        public int Id { get; set; }
        public string LectureId { get; set; }
        public int Panel { get; set; }
        public string Position { get; set; }
    }
}