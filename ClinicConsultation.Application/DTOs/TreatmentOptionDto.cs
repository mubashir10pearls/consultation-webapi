namespace ClinicConsultation.Application.DTOs
{
    public class TreatmentOptionDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public decimal EstimatedCostMin { get; set; }

        public decimal EstimatedCostMax { get; set; }

        public string? Unit { get; set; }
    }
}
