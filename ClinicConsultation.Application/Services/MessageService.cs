using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Exceptions;
using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using ClinicConsultation.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ClinicConsultation.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(
            IMessageRepository messageRepository,
            ILogger<MessageService> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        public async Task<MessageDto> AddMessageAsync(Guid consultationId, CreateMessageDto dto)
        {
            var exists = await _messageRepository.ConsultationExistsAsync(consultationId);
            if (!exists)
                throw new NotFoundException(nameof(Consultation), consultationId);

            var message = new Message
            {
                Id = Guid.NewGuid(),
                ConsultationId = consultationId,
                Role = dto.Role,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation("Added {Role} message to consultation {ConsultationId}.",
                dto.Role, consultationId);

            return MapToDto(message);
        }

        public async Task<List<MessageDto>> GetMessagesAsync(Guid consultationId)
        {
            var messages = await _messageRepository.GetByConsultationIdAsync(consultationId);
            return messages.Select(MapToDto).ToList();
        }

        private static MessageDto MapToDto(Message m) => new()
        {
            Id = m.Id,
            ConsultationId = m.ConsultationId,
            Role = m.Role,
            Content = m.Content,
            CreatedAt = m.CreatedAt
        };
    }
}
