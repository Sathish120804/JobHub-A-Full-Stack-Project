using System.Runtime.CompilerServices;
using JobHub.API.DTOs;
using JobHub.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace JobHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authservice;
    public AuthController(IAuthService service)
    {
        _authservice = service;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authservice.RegisterAsync(dto);
        if (!result)
        {
            return BadRequest("Email already Exist");

        }
        return Ok("User registered successfully.");

    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authservice.LoginAsync(dto);

        if (token == null)
            return Unauthorized("Invalid email or password.");

        return Ok(new
        {
            token
        });
    }
}