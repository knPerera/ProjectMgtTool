using System;
using System.ComponentModel.DataAnnotations;

namespace SEP.Models
{
    public class PresentationSchedule
    {

        [Key]
        public int Id { get; set; }
        public string Presentation { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public int TimePerGroup { get; set; }
        public string Unit { get; set; }
        [Display(Name = "Start From")]
        [DataType(DataType.Time)]
        public DateTime StartTime{ get; set; }
        [Display(Name = "To")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int Interval { get; set; }
        public int NoOfGroups { get; set; }
        public int NoOfPanels { get; set; }

    }
}