using System.ComponentModel.DataAnnotations;

namespace ShiftOn.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ShiftName { get; set; }

        public Guid ScheduleId { get; set; }

        public Schedule Schedule { get; set; }
    }
}
