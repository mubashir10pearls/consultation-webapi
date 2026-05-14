using ClinicConsultation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicConsultation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _service;

    public RecommendationController(IRecommendationService service)
    {
        _service = service;
    }

    [HttpPost("{consultationId:guid}/generate")]
    public async Task<IActionResult> Generate(Guid consultationId)
    {        
        var result = await _service.GenerateAsync(consultationId);
        return Ok(result);
    }

    [HttpGet("{consultationId:guid}")]
    public async Task<IActionResult> Get(Guid consultationId)
    {
        var result = await _service.GetByConsultationIdAsync(consultationId);
        if (result == null)
            return NotFound(new { message = "Recommendation not found." });

        return Ok(result);
    }
}
