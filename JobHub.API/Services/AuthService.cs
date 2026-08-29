using JobHub.API.DTOs;
using JobHub.API.Data;
using Microsoft.EntityFrameworkCore;
using JobHub.API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
//claims nothing but the piece of info 
//containing the authenticated user info..

namespace JobHub.API.Services;

public class AuthService : IAuthService
{
    //injecting the dependency constructor injection
    private readonly ApplicationDbcontext _dbcontext;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbcontext dbcontext, IConfiguration configuration)
    {
        _dbcontext = dbcontext;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        //step 1 for login the user
        //We need to check:Does a user with this email AND password exist?
        var user = await _dbcontext.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == dto.PasswordHash
        );
        if (user == null)
        {
            return null;
        }

        var claims = new List<Claim>
        // Create JWT claims to store authenticated user's ID, name, email, and role.
        // These claims are included in the token and used for user identification and authorization.
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Name),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

        var key = new SymmetricSecurityKey//here this is the secret key 
        //but i expect the key from the appsettings.json
        (
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credientials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //SecurityAlgorithms--->contains the secret key+Signing algo
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credientials);

        //Last step — Convert Token to String
        return new JwtSecurityTokenHandler()
            .WriteToken(token);

        // throw new NotImplementedException();
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        //1-checking existing user
        var existingUser = await _dbcontext.Users
        .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existingUser != null)
        {
            return false;
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password,
            Role = dto.Role
        };

        _dbcontext.Users.Add(user);
        await _dbcontext.SaveChangesAsync();

        return true;
    }

    //we need a the jwt keys which contain the secret key of our jwt
    //-->SymmetricSecurityKey

}