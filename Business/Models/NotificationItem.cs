using System.Data;

namespace Business.Models;

public class NotificationItem
{
    public string ProjectName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; } = null!;
}
