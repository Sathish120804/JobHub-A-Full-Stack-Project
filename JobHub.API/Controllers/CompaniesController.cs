using JobHub.API.DTOs;
using JobHub.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyservice;
    public CompaniesController(ICompanyService companyservice)
    {
        _companyservice=companyservice;
    }
    //get method
    [HttpGet]
    public async Task<ActionResult<List<CompanyResponseDto>>> GetAll()
    {
        var companies=await _companyservice.GetAllAsync();
        return Ok(companies);
    }
     [HttpGet("{id}")]
    public async Task<ActionResult<CompanyResponseDto>> GetById(int id)
    {
        var company = await _companyservice.GetByIDAsync(id); 

        if (company == null)
            return NotFound();

        return Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyResponseDto>> Create(
        [FromBody] CreateCompanyDto dto)
    {
        var company = await _companyservice.CreateAsync(dto);

        return Ok(company);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] CreateCompanyDto dto)
    {
        var updated = await _companyservice.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _companyservice.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}