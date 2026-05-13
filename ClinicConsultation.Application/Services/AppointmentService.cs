using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Exceptions;
using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ClinicConsultation.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IGenericRepository<Appointment> _repository;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(
            IGenericRepository<Appointment> repository,
            ILogger<AppointmentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return appointments.Select(MapToDto).ToList();
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            // AppointmentDate is now DateTime (not string) — DateTime.Parse() removed entirely
            var appointment = new Appointment
            {
                ConsultationId = dto.ConsultationId,
                Treatment = dto.Treatment,
                AppointmentDate = dto.AppointmentDate,
                Location = dto.Location,
                TargetArea = dto.TargetArea,
                EstimateDuration = dto.EstimateDuration,
                Provider = dto.Provider
            };

            await _repository.AddAsync(appointment);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Created appointment {Id} for consultation {ConsultationId}.",
                appointment.Id, appointment.ConsultationId);

            return MapToDto(appointment);
        }

        private static AppointmentDto MapToDto(Appointment a) => new()
        {
            Id = a.Id,
            ConsultationId = a.ConsultationId,
            Treatment = a.Treatment,
            AppointmentDate = a.AppointmentDate,
            Location = a.Location,
            TargetArea = a.TargetArea,
            EstimateDuration = a.EstimateDuration,
            Provider = a.Provider
        };
    }
}
