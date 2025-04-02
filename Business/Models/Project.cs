namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TimeLeft { get; set; } = null!;
}
