using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftOn.Models
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ScheduleId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int ShiftId { get; set; }

        [Required]
        public int VacationRequestId { get; set; }

        public ICollection<VacationRequest> VacationRequests { get; set; }
        public ICollection<Shift> Shifts { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
