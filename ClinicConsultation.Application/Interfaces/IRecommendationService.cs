using ClinicConsultation.Application.DTOs;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<RecommendationDto> GenerateAsync(Guid consultationId);
        Task<RecommendationDto?> GetByConsultationIdAsync(Guid consultationId);
    }
}
