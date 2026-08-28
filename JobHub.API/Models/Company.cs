namespace JobHub.API.Models;
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public ICollection<Job> Jobs { get; set; }
        = new List<Job>();
}