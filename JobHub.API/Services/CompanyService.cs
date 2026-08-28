using JobHub.API.Data;
using JobHub.API.DTOs;
using JobHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobHub.API.Services;

public class CompanyService : ICompanyService
{
    private readonly ApplicationDbcontext _dbcontext;
    public CompanyService(ApplicationDbcontext dbcontext)
    {
        _dbcontext=dbcontext;
    }
    public async Task<CompanyResponseDto> CreateAsync(CreateCompanyDto dto)
    {
         //ipo create panniyachu companies lam
        var company=new Company
        {
            Name = dto.Name,
            Description = dto.Description,
            Location = dto.Location,
            Website = dto.Website
            //request body vara json data va object ah dto ngra variable la store pannum...
        };
        _dbcontext.Companies.Add(company);
        await _dbcontext.SaveChangesAsync();
       //ipo nama return response return panrom 
       return new CompanyResponseDto
       {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Location = company.Location,
            Website = company.Website
       };
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var  company=await _dbcontext.Companies.FindAsync(id);
        if(company==null)
        return false;
        _dbcontext.Companies.Remove(company);
        await _dbcontext.SaveChangesAsync();
        return true;
        throw new NotImplementedException();
    }

    public async Task<List<CompanyResponseDto>> GetAllAsync()
    {
        return await _dbcontext.Companies
            .Select(company => new CompanyResponseDto//which is called projection
            {
                Id = company.Id,
                Name = company.Name,
                Description=company.Description,
                Location=company.Location,
                Website=company.Website
            
            })
            .ToListAsync();
    }

    public async Task<CompanyResponseDto?> GetByIDAsync(int id)
    {
        return await _dbcontext.Companies
        .Where(company=>company.Id==id)
        .Select(company=>new CompanyResponseDto
        {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Location = company.Location,
                Website = company.Website   
        })
        .FirstOrDefaultAsync();
        throw new NotImplementedException();
    }

    public Task GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(int id, CreateCompanyDto dto)
    {
        var company =await _dbcontext.Companies.FindAsync(id);
        if(company==null)
        return false;
         company.Name = dto.Name;
        company.Description = dto.Description;
        company.Location = dto.Location;
        company.Website = dto.Website;

        await _dbcontext.SaveChangesAsync();

        return true;
        throw new NotImplementedException();
    }
}
