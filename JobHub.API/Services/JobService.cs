using JobHub.API.DTOs;
using JobHub.API.Models;
using JobHub.API.Data;
namespace JobHub.API.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public class JobService : IJobService//here automatically
//implemented the interface methods in JobService.cs
{
    private readonly ApplicationDbcontext _dbContext;
    public JobService(ApplicationDbcontext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<JobResponseDto> CreateAsync(CreateJobDto dto)
    {
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            Salary = dto.Salary,
            CompanyId = dto.CompanyId
        };
        _dbContext.Jobs.Add(job);//trlling ef core that new entity have been added..
        await _dbContext.SaveChangesAsync();//permanent ahh nama db la store panrathu
        return await GetByIdAsync(job.Id) ??
        throw new Exception("Job creation failed");
        //fetching the newly created record 
        // lets us return the properly projected JobResponseDto.
    }

    public async Task<List<JobResponseDto>> GetAllAsync()
    {
        var jobs = await _dbContext.Jobs
        .Include(j => j.Company)
        .Select(j => new JobResponseDto
        {
            Id = j.Id,
            Title = j.Title,
            Description = j.Description,
            Location = j.Location,
            Salary = j.Salary,
            CompanyName = j.Company!.Name
        })
        .ToListAsync();

        return jobs;
    }

    public async Task<JobResponseDto?> GetByIdAsync(int id)
    {
        return await _dbContext.Jobs
            .Include(j => j.Company)
            .Where(j => j.Id == id)
            .Select(j => new JobResponseDto//projection 
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                Salary = j.Salary,
                CompanyName = j.Company!.Name
            })
            .FirstOrDefaultAsync();//Give me the first matching record, or null if nothing exists.
    }

    public async Task<bool> UpdateAsync(int id, CreateJobDto dto)
    {
        var job = await _dbContext.Jobs.FindAsync(id);
        if (job == null)
            return false;

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.Location = dto.Location;
        job.Salary = dto.Salary;
        job.CompanyId = dto.CompanyId;

        await _dbContext.SaveChangesAsync();

        return true;

        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var job = await _dbContext.Jobs.FindAsync(id);

        if (job == null)
            return false;

        _dbContext.Jobs.Remove(job);

        await _dbContext.SaveChangesAsync();

        return true;
    }


}