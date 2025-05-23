﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class NotificationEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public string Message { get; set; } = null!;
    public string Image { get; set; } = null!;

    [ForeignKey(nameof(NotificationType))]
    public int NotificationTypeId { get; set; }
    public virtual NotificationTypeEntity NotificationType { get; set; } = null!;

    [ForeignKey(nameof(NotificationTarget))]
    public int NotificationTargetId { get; set; }
    public virtual NotificationTargetEntity NotificationTarget { get; set; } = null!;

    public virtual ICollection<UserDismissNotificationEntity> DismissedNotifications { get; set; } = [];
}
