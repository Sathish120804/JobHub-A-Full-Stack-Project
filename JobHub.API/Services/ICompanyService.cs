using JobHub.API.DTOs;
namespace JobHub.API.Services;
public interface ICompanyService
{
    Task<List<CompanyResponseDto>> GetAllAsync();
     Task<CompanyResponseDto?> GetByIDAsync(int id);
    Task<CompanyResponseDto>CreateAsync(CreateCompanyDto dto);
    Task <bool> UpdateAsync(int id,CreateCompanyDto dto);

    Task<bool> DeleteAsync (int id);
    Task GetByIdAsync(int id);
}