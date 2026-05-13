using System.ComponentModel.DataAnnotations;

namespace ClinicConsultation.Application.DTOs
{
    public class CreateAppointmentDto
    {
        [Required(ErrorMessage = "ConsultationId is required.")]
        public Guid ConsultationId { get; set; }

        [Required(ErrorMessage = "Treatment is required.")]
        [MaxLength(200, ErrorMessage = "Treatment name cannot exceed 200 characters.")]
        public string Treatment { get; set; } = string.Empty;

        // Changed from string to DateTime — ASP.NET Core model binding handles ISO 8601,
        // eliminating the unsafe DateTime.Parse() call that was in AppointmentService.
        [Required(ErrorMessage = "Appointment date is required.")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string Location { get; set; } = string.Empty;

        [MaxLength(200)]
        public string TargetArea { get; set; } = string.Empty;

        [MaxLength(50)]
        public string EstimateDuration { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Provider { get; set; } = string.Empty;
    }
}
