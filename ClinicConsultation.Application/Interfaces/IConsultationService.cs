using ClinicConsultation.Application.DTOs;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IConsultationService
    {
        Task<List<ConsultationDto>> GetAllAsync();
        Task<ConsultationDto> GetByIdAsync(Guid id);
        Task<ConsultationDto> CreateAsync(CreateConsultationDto dto);
        Task UpdateStatusAsync(Guid id, UpdateConsultationStatusDto dto);
    }
}
