using JobHub.API.Models;
using Microsoft.EntityFrameworkCore;
namespace JobHub.API.Data;

public class ApplicationDbcontext : DbContext
{
    //we need to create a constructor that takes DbContextOptions and 
    // passes it to the base class constructor
    public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
    {
        //the paramater we passed is the options that we will use to configure the database connection
        //which is inherited from the base class constructor
    }
    //we  need a DbSet for each model class that we want to map to a database table
    public DbSet<User> Users => Set<User>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Application> Applications => Set<Application>();
}

