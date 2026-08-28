namespace JobHub.API.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Candidate";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Application> Applications { get; set; }
        = new List<Application>();
}