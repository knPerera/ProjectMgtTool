using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SEP.Models
{
    public class EvaluationPanel
    {
        [Key]
        public int id { get; set; }
        public string panel { get; set; }
        public string lecturers { get; set; }
    }
}