using System.Data;

namespace Business.Models;

public class NotificationItemForm
{
    public int NotificationTypeId { get; set; }
    public int NotificationTargetId { get; set; }
    public string Message { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? UserId { get; set; }
}
