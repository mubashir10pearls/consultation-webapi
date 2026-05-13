using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicConsultation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationsController : ControllerBase
{
    private readonly IConsultationService _service;

    public ConsultationsController(IConsultationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    // Added: GET /api/consultations/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // NotFoundException → 404 via GlobalExceptionMiddleware
        var consultation = await _service.GetByIdAsync(id);
        return Ok(consultation);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConsultationDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // Added: PATCH /api/consultations/{id}/status
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateConsultationStatusDto dto)
    {
        // NotFoundException → 404 via GlobalExceptionMiddleware
        await _service.UpdateStatusAsync(id, dto);
        return NoContent();
    }
}
