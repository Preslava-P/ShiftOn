using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftOn.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Required]
        public Guid ScheduleId { get; set; }
        [Required]
        public int VacationRequestId { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<VacationRequest> VacationRequests { get; set;}
    }
}
