using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Exceptions;
using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClinicConsultation.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly ILogger<RecommendationService> _logger;

        public RecommendationService(
            IRecommendationRepository recommendationRepository,
            ILogger<RecommendationService> logger)
        {
            _recommendationRepository = recommendationRepository;
            _logger = logger;
        }

        public async Task<RecommendationDto?> GetByConsultationIdAsync(Guid consultationId)
        {
            var rec = await _recommendationRepository.GetByConsultationIdAsync(consultationId);
            return rec == null ? null : MapToDto(rec);
        }

        public async Task<RecommendationDto> GenerateAsync(Guid consultationId)
        {
            var consultation = await _recommendationRepository.GetConsultationWithMessagesAsync(consultationId)
                ?? throw new NotFoundException(nameof(Consultation), consultationId);

            // Mock AI logic — placeholder for future OpenAI integration
            var recommendation = new Recommendation
            {
                Id = Guid.NewGuid(),
                ConsultationId = consultationId,
                Summary = $"Summary for {consultation.PrimaryConcern}",
                RecommendedTreatment = "Basic Treatment Plan",
                Reasoning = "Based on symptoms and chat history",
                AiInsight = "AI suggests early-stage treatment",
                Confidence = 0.87,
                Treatments = new List<TreatmentOption>
                {
                    new TreatmentOption
                    {
                        Id = Guid.NewGuid(),
                        Name = "Standard Care",
                        Description = "Basic treatment option",
                        Priority = 1,
                        EstimatedCostMin = 100,
                        EstimatedCostMax = 200,
                        Unit = "USD"
                    },
                    new TreatmentOption
                    {
                        Id = Guid.NewGuid(),
                        Name = "Advanced Care",
                        Description = "Specialist treatment",
                        Priority = 2,
                        EstimatedCostMin = 300,
                        EstimatedCostMax = 600,
                        Unit = "USD"
                    }
                }
            };

            await _recommendationRepository.AddAsync(recommendation);
            await _recommendationRepository.SaveChangesAsync();

            _logger.LogInformation("Generated recommendation {Id} for consultation {ConsultationId}.",
                recommendation.Id, consultationId);

            return MapToDto(recommendation);
        }

        private static RecommendationDto MapToDto(Recommendation rec) => new()
        {
            Id = rec.Id,
            ConsultationId = rec.ConsultationId,
            Summary = rec.Summary,
            RecommendedTreatment = rec.RecommendedTreatment,
            Reasoning = rec.Reasoning,
            AiInsight = rec.AiInsight,
            Confidence = rec.Confidence,
            Treatments = rec.Treatments?.Select(t => new TreatmentOptionDto
            {
                Name = t.Name,
                Description = t.Description,
                Priority = t.Priority,
                EstimatedCostMin = t.EstimatedCostMin,
                EstimatedCostMax = t.EstimatedCostMax,
                Unit = t.Unit
            }).ToList() ?? new List<TreatmentOptionDto>()
        };
    }
}
