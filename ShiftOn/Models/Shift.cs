using System.ComponentModel.DataAnnotations;

namespace ShiftOn.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }

        [Required]
        public string ShiftName { get; set; }

        [Required]
        public Guid ScheduleId { get; set; }

        public ICollection<Schedule> Schedules { get; set;}
    }
}
