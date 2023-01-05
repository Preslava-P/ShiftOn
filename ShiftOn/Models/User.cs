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
        public string Lastname { get; set; }

        public int VacationRequestId { get; set; }
        public Guid ScheduleId { get; set; }

        public ICollection<VacationRequest> VacationRequests { get; set; }  
        public Schedule Schedule { get; set; }
    }
}
