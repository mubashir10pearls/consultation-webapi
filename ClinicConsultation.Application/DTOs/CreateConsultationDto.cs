using System.ComponentModel.DataAnnotations;

namespace ClinicConsultation.Application.DTOs
{
    public class CreateConsultationDto
    {
        [Required(ErrorMessage = "Patient name is required.")]
        [MaxLength(200, ErrorMessage = "Patient name cannot exceed 200 characters.")]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Primary concern is required.")]
        [MaxLength(500, ErrorMessage = "Primary concern cannot exceed 500 characters.")]
        public string PrimaryConcern { get; set; } = string.Empty;
    }
}
