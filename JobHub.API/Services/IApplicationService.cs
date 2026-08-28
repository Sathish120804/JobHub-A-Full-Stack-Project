using JobHub.API.DTOs;

namespace JobHub.API.Services;

public interface IApplicationService
{
    Task<List<ApplicationResponseDto>> GetAllAsync();

    Task<ApplicationResponseDto?> GetByIdAsync(int id);

    Task<ApplicationResponseDto> CreateAsync(CreateApplicationDto dto);

    Task<bool> UpdateStatusAsync(int id, string status);
}