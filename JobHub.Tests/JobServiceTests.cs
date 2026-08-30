// First step: to set the initial fake database in memory for testing

using JobHub.API.Data;
using JobHub.API.Models;
using JobHub.API.Services;
using Microsoft.EntityFrameworkCore;

public class JobServiceTest
{
    // This method creates a fake/in-memory database
    // for our unit tests.
    private ApplicationDbcontext GetDbcontext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbcontext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbcontext(options);
    }


    [Fact]
    public async Task GetByIdAsync_ShouldReturnJob_WhenJobExists()
    {
        // =========================================================
        // ARRANGE
        // =========================================================

        // Given
        var dbcontext = GetDbcontext();

        // We are creating our fake/dummy database in memory.
        //
        // IMPORTANT:
        // This is NOT our real SQL Server database.
        //
        // Production:
        //
        // SQL Server
        //     ↓
        // ApplicationDbContext
        //
        // Testing:
        //
        // InMemory Database
        //     ↓
        // ApplicationDbContext


        // ---------------------------------------------------------
        // Create a fake Company
        // ---------------------------------------------------------

        var company = new Company
        {
            Id = 1,
            Name = "ABC Technologies"
        };

        dbcontext.Companies.Add(company);


        // ---------------------------------------------------------
        // Create a fake Job
        // ---------------------------------------------------------

        var job = new Job
        {
            Id = 1,
            Title = "Backend Developer",
            Description = "Build APIs",
            Location = "Chennai",
            Salary = 50000,

            // Foreign Key
            CompanyId = 1,

            // Navigation Property
            Company = company
        };

        dbcontext.Jobs.Add(job);


        // Save our fake data into the InMemory database
        await dbcontext.SaveChangesAsync();


        // ---------------------------------------------------------
        // Create JobService
        // ---------------------------------------------------------

        var jobservice = new JobService(dbcontext);

        /*
            Namma actual JobService constructor:

            public JobService(ApplicationDbcontext dbContext)
            {
                _dbContext = dbContext;
            }

            Production-la:

            DI Container
                 ↓
            JobService
                 ↓
            ApplicationDbContext


            Test-la:

            Test Code
                 ↓
            JobService
                 ↓
            Fake ApplicationDbContext
        */


        // =========================================================
        // ACT
        // =========================================================

        // We call the actual method that we want to test.

        var result = await jobservice.GetByIdAsync(1);


        // =========================================================
        // ASSERT
        // =========================================================

        // Check whether a Job was actually returned.
        Assert.NotNull(result);

        // Check whether the returned Job has the expected ID.
        Assert.Equal(1, result.Id);

        // Check whether the returned Job has the expected title.
        Assert.Equal("Backend Developer", result.Title);

        // Check whether the Company information was loaded correctly.
        Assert.Equal("ABC Technologies", result.CompanyName);
    }


    // =============================================================
    // SECOND TEST
    // What if the Job does NOT exist?
    // =============================================================

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenJobDoesNotExist()
    {
        // =========================================================
        // ARRANGE
        // =========================================================

        // Create a completely new fake database.
        var context = GetDbcontext();

        // Create the JobService and inject our fake DbContext.

        var service = new JobService(context);


        // =========================================================
        // ACT
        // =========================================================

        // We are asking for Job ID 999.
        //
        // But we haven't inserted Job ID 999
        // into our fake database.

        var result = await service.GetByIdAsync(999);


        // =========================================================
        // ASSERT
        // =========================================================

        // Since Job 999 doesn't exist,
        // GetByIdAsync() should return null.

        Assert.Null(result);
    }
}