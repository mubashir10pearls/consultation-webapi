using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicConsultation.Domain.Entities
{
    public class TreatmentOption
    {
        public Guid Id { get; set; }

        public Guid RecommendationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Priority { get; set; }

        public decimal EstimatedCostMin { get; set; }

        public decimal EstimatedCostMax { get; set; }

        public string? Unit { get; set; }

        public Recommendation Recommendation { get; set; }
    }
}
