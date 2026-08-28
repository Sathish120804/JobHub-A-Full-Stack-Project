using JobHub.API.DTOs;
using JobHub.API.Services;
using Microsoft.AspNetCore.Mvc;
namespace JobHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
//inheritence because it gives all the methods of
//ok(),not found()
//ControllerBase provides methods and properties 
// needed for handling HTTP requests and generating API responses, 
// without MVC view support.
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }
    //here we depend on the Ijobservice where we define all methods
    //of get,post(The controller depends on the interface)
    //here constructor dependency injection been used
    [HttpGet]
    public async Task<ActionResult<JobResponseDto>> GetAll()
    {
        var jobs = await _jobService.GetAllAsync();
        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobResponseDto>> GetById(int id)
    {
        var job = await _jobService.GetByIdAsync(id);
        if (job == null)
        {
            return NotFound();

        }
        else
        {
            return Ok(job);
        }
    }
    [HttpPost]
    public async Task<ActionResult<JobResponseDto>> Create([FromBody] CreateJobDto dto)
    {
        var job = await _jobService.CreateAsync(dto);

        return Ok(job);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
    int id,
    [FromBody] CreateJobDto dto)
    {
        var updated = await _jobService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _jobService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}