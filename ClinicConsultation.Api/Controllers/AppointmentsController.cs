using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicConsultation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentsController(IAppointmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }
}
