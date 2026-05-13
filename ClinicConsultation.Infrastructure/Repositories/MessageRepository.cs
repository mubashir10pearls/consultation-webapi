using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using ClinicConsultation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicConsultation.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetByConsultationIdAsync(Guid consultationId)
        {
            return await _context.Messages
                .Where(m => m.ConsultationId == consultationId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<bool> ConsultationExistsAsync(Guid consultationId)
        {
            return await _context.Consultations
                .AnyAsync(c => c.Id == consultationId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
