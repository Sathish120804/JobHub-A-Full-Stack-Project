using JobHub.API.DTOs;
using JobHub.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(
        IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ApplicationResponseDto>>> GetAll()
    {
        var applications =
            await _applicationService.GetAllAsync();

        return Ok(applications);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationResponseDto>> GetById(
        int id)
    {
        var application =
            await _applicationService.GetByIdAsync(id);

        if (application == null)
            return NotFound();

        return Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationResponseDto>> Create(
        [FromBody] CreateApplicationDto dto)
    {
        var application =
            await _applicationService.CreateAsync(dto);

        return Ok(application);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] string status)
    {
        var updated =
            await _applicationService.UpdateStatusAsync(
                id,
                status);

        if (!updated)
            return NotFound();

        return NoContent();
    }
}