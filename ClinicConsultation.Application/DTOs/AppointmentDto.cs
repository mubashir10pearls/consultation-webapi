namespace ClinicConsultation.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid ConsultationId { get; set; }
        public string Treatment { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string TargetArea { get; set; } = string.Empty;
        public string EstimateDuration { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
    }
}
