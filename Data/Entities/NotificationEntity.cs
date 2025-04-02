﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class NotificationEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public string Message { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;


    [ForeignKey(nameof(NotificationType))]
    public int NotificationTypeId { get; set; }
    public virtual NotificationTypeEntity NotificationType { get; set; } = null!;

    [ForeignKey(nameof(NotificationTarget))]
    public int NotificationTargetId { get; set; }
    public virtual NotificationTargetEntity NotificationTarget { get; set; } = null!;
}
