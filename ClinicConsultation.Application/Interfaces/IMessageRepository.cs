using ClinicConsultation.Domain.Entities;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetByConsultationIdAsync(Guid consultationId);
        Task AddAsync(Message message);
        Task<bool> ConsultationExistsAsync(Guid consultationId);
        Task SaveChangesAsync();
    }
}
