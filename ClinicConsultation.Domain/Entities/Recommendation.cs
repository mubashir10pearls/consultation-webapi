namespace ClinicConsultation.Domain.Entities
{
    public class Recommendation
    {
        public Guid Id { get; set; }

        public Guid ConsultationId { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string RecommendedTreatment { get; set; } = string.Empty;

        public string Reasoning { get; set; } = string.Empty;

        public string? AiInsight { get; set; }

        public double? Confidence { get; set; }

        public Consultation Consultation { get; set; }
        public ICollection<TreatmentOption> Treatments { get; set; }
    = new List<TreatmentOption>();
    }
}
