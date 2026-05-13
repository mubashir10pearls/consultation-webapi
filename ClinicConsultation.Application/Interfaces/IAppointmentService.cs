using ClinicConsultation.Application.DTOs;

namespace ClinicConsultation.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);
    }
}
