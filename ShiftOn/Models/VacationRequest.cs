using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShiftOn.Models
{
    public class VacationRequest
    {
        [Key]
        public int VacationRequestId { get; set; }

        [Required]
        [DisplayName("Request date")]
        public DateTime Date { get; set; }

        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ScheduleId { get; set; }

        public User User { get; set; }  
        public Schedule Schedule { get; set; } 
    }
}
