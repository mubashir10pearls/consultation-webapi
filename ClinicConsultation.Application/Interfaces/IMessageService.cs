using ClinicConsultation.Application.DTOs;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> AddMessageAsync(Guid consultationId, CreateMessageDto dto);

        Task<List<MessageDto>> GetMessagesAsync(Guid consultationId);
    }
}
