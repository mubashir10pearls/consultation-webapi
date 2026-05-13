using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using ClinicConsultation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicConsultation.Infrastructure.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _context;

        public RecommendationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recommendation?> GetByConsultationIdAsync(Guid consultationId)
        {
            return await _context.Recommendations
                .Include(r => r.Treatments)
                .FirstOrDefaultAsync(r => r.ConsultationId == consultationId);
        }

        public async Task<Consultation?> GetConsultationWithMessagesAsync(Guid consultationId)
        {
            return await _context.Consultations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == consultationId);
        }

        public async Task AddAsync(Recommendation recommendation)
        {
            await _context.Recommendations.AddAsync(recommendation);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
