using System;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        [Required]
        public int id { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public DateTime start_date { get; set; }
        [Required]
        public DateTime end_date { get; set; }
    }
}