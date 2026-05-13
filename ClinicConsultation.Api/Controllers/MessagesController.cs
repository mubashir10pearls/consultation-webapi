using ClinicConsultation.Application.DTOs;
using ClinicConsultation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicConsultation.Api.Controllers;

[ApiController]
[Route("api/consultations/{consultationId:guid}/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _service;

    public MessagesController(IMessageService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages(Guid consultationId)
    {
        var result = await _service.GetMessagesAsync(consultationId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddMessage(Guid consultationId, [FromBody] CreateMessageDto dto)
    {
        // NotFoundException for unknown consultation → 404 via GlobalExceptionMiddleware
        // Inline try/catch removed — all exception handling is centralised in middleware
        var result = await _service.AddMessageAsync(consultationId, dto);
        return Ok(result);
    }
}
