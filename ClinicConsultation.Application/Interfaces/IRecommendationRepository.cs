using ClinicConsultation.Domain.Entities;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IRecommendationRepository
    {
        Task<Recommendation?> GetByConsultationIdAsync(Guid consultationId);
        Task<Consultation?> GetConsultationWithMessagesAsync(Guid consultationId);
        Task AddAsync(Recommendation recommendation);
        Task SaveChangesAsync();
    }
}
