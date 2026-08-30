using JobHub.API.Data;
using JobHub.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using JobHub.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//swagger Ui documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding service of controllers to the container 
builder.Services.AddControllers();
//adding the service of Dbcontext 
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApplicationDbcontext>
//here we reg our dbcontext service with dependency injection container
builder.Services.AddScoped<IJobService, JobService>();
//scoped panirukom
//Scoped    → one per HTTP request
builder.Services.AddScoped<ICompanyService, CompanyService>();
//adding the service with Scoped Lifeline for company 
builder.Services.AddScoped<IApplicationService, ApplicationService>();
//adding the application service in our program.cs
builder.Services.AddScoped<IAuthService, AuthService>();
//adding the service of jwt authenticationn

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        //When someone sends a JWT, what things should you check?
        // Is issuer correct?
        // Is audience correct?
        // Is token expired?
        // Is signature valid?
        ValidIssuer=builder.Configuration["Jwt:Issuer"],
        ValidAudience=builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    
    });
builder.Services.AddAuthorization();
var app = builder.Build();//this line builds the application and 
//returns a WebApplication instance that 
// we can use to configure the HTTP request pipeline.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//adding the middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// HTTP Request
//      ↓
// UseHttpsRedirection
//      ↓
// UseAuthentication()
//      ↓
// "Who is this user?"
//      ↓
// UseAuthorization()
//      ↓
// "Is this user allowed?"
//      ↓
// MapControllers()
//      ↓
// Controller

