using JobHub.API.Data;
using JobHub.API.DTOs;
using JobHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JobHub.API.Services;

public class ApplicationService : IApplicationService
{
    private readonly ApplicationDbcontext _dbcontext;

    public ApplicationService(ApplicationDbcontext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<List<ApplicationResponseDto>> GetAllAsync()
    {
        return await _dbcontext.Applications
           // Job edukkumbodhu, related Company data-vum load pannu.
            .Include(a => a.Job)//we are asking ef core to load the job and userdata
            .Include(a => a.User)
            .Select(a => new ApplicationResponseDto
            //Select() = "Existing data-la irundhu required fields-ah eduthu,
            //  pudhu object/shape create pannradhu
            {
                //Without loading the related data, 
                // we can't safely access:This is called eager loading.
                Id = a.Id,
                JobId = a.JobId,
                JobTitle = a.Job!.Title,
                UserId = a.UserId,
                UserName = a.User!.Name,
                Status = a.Status,
                AppliedAt = a.AppliedAt
            })
            .ToListAsync();
    }

    public async Task<ApplicationResponseDto?> GetByIdAsync(int id)
    {
        return await _dbcontext.Applications
            .Include(a => a.Job)
            .Include(a => a.User)
            .Where(a => a.Id == id)//used to filter with the id
            .Select(a => new ApplicationResponseDto
            {
                Id = a.Id,
                JobId = a.JobId,
                JobTitle = a.Job!.Title,
                UserId = a.UserId,
                UserName = a.User!.Name,
                Status = a.Status,
                AppliedAt = a.AppliedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ApplicationResponseDto> CreateAsync(
        CreateApplicationDto dto)
    {
        var application = new Application
        {
            JobId = dto.JobId,
            UserId = dto.UserId,
            Status = "Applied"
        };

        _dbcontext.Applications.Add(application);

        await _dbcontext.SaveChangesAsync();

        return await GetByIdAsync(application.Id)
            ?? throw new Exception("Application creation failed.");
    }

    public async Task<bool> UpdateStatusAsync(
        int id,
        string status)
    {
        var application =
            await _dbcontext.Applications.FindAsync(id);

        if (application == null)
            return false;

        application.Status = status;

        await _dbcontext.SaveChangesAsync();

        return true;
    }
}
//Application
    // │
    // ├── Job
     //│    └── Title
    // │
    // └── User
      //    └── Name


      //a.Job.Title
    // a.User.Name

    // This is called eager loading.
    // Eager loading loads related entities as part of the initial database query, 
    // commonly using Include() in EF Core.