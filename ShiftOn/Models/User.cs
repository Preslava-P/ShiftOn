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
        [DisplayName("Име")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("График")]
        public Guid ScheduleId { get; set; }
        [Required]
        [DisplayName("Почивен ден")]
        public int VacationRequestId { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<VacationRequest> VacationRequests { get; set;}
    }
}
