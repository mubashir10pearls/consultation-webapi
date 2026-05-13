namespace ClinicConsultation.Application.DTOs
{
    public class RecommendationDto
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Summary { get; set; }

        public string RecommendedTreatment { get; set; }

        public string Reasoning { get; set; }

        public string? AiInsight { get; set; }

        public double? Confidence { get; set; }

        public List<TreatmentOptionDto> Treatments { get; set; }
    }
}
