using Authentication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UserDismissNotificationEntity
{
    [Key]
    public int Id { get; set; }


    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public virtual AppUser User { get; set; } = null!;


    [ForeignKey(nameof(Notification))]
    public string NotificationId { get; set; } = null!;
    public virtual NotificationEntity Notification { get; set; } = null!;
}
