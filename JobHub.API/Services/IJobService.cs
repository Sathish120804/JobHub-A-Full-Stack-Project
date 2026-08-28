namespace JobHub.API.Services;
using JobHub.API.DTOs;
public interface IJobService
{
    //here interface is the abstraction which 
    //we see where how the implementation is done in JobService.cs
    //Any IJobService implementation 
    // must have a GetAllAsync() method that returns jobs asynchronously.
    Task<List<JobResponseDto>> GetAllAsync();//return a client of all jobs
    Task<JobResponseDto?> GetByIdAsync(int id);//idhu only for onejob ku mattum
    Task<JobResponseDto> CreateAsync(CreateJobDto dto);//Client-->API(Create the job)-->Service(Create the job)
    //-->Repository(Create the job)
    Task<bool> UpdateAsync(int id, CreateJobDto dto);
    Task<bool> DeleteAsync(int id);

}