using System.ComponentModel.DataAnnotations;

namespace ClinicConsultation.Application.DTOs
{
    public class UpdateConsultationStatusDto
    {
        [Required]
        [RegularExpression("^(Pending|Booked|Completed)$",
            ErrorMessage = "Status must be Pending, Booked, or Completed.")]
        public string Status { get; set; } = string.Empty;
    }
}
