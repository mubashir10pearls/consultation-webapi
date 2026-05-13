using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Exceptions;
using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using ClinicConsultation.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ClinicConsultation.Application.Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IGenericRepository<Consultation> _repository;
        private readonly ILogger<ConsultationService> _logger;

        public ConsultationService(
            IGenericRepository<Consultation> repository,
            ILogger<ConsultationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<ConsultationDto>> GetAllAsync()
        {
            var consultations = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} consultations.", consultations.Count);
            return consultations.Select(MapToDto).ToList();
        }

        public async Task<ConsultationDto> GetByIdAsync(Guid id)
        {
            var consultation = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Consultation), id);
            return MapToDto(consultation);
        }

        public async Task<ConsultationDto> CreateAsync(CreateConsultationDto dto)
        {
            var consultation = new Consultation
            {
                PatientName = dto.PatientName,
                PrimaryConcern = dto.PrimaryConcern,
                Status = ConsultationStatus.Pending.ToString()
            };

            await _repository.AddAsync(consultation);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Created consultation {Id} for patient '{PatientName}'.",
                consultation.Id, consultation.PatientName);

            return MapToDto(consultation);
        }

        public async Task UpdateStatusAsync(Guid id, UpdateConsultationStatusDto dto)
        {
            var consultation = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Consultation), id);

            consultation.Status = dto.Status;
            _repository.Update(consultation);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Updated consultation {Id} status to '{Status}'.", id, dto.Status);
        }

        private static ConsultationDto MapToDto(Consultation c) => new()
        {
            Id = c.Id,
            PatientName = c.PatientName,
            PrimaryConcern = c.PrimaryConcern,
            Status = c.Status,
            CreatedAt = c.CreatedAt
        };
    }
}
