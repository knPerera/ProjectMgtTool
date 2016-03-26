using System.ComponentModel.DataAnnotations;

namespace SEP.Models
{
    public class PresentationGroups
    {
        [Key]
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int PanelNo { get; set; }
        public string GroupId { get; set; }
        public string ProjectId { get; set; }
        public string Supervisor { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}