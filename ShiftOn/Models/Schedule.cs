using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftOn.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ScheduleId { get; set; }

        [Required]
        public int ShiftId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int VacationRequestId { get; set; }

        public Shift Shift { get; set; }
        public User User { get; set; }
    }
}
