using JobHub.API.DTOs;

namespace JobHub.API.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDto dto);
    Task<bool> RegisterAsync(RegisterDto dto);

}