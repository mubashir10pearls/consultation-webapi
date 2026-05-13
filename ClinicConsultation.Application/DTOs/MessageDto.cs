namespace ClinicConsultation.Application.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Role { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
