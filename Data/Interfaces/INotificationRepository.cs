using Data.Entities;
using Domain.Models;
using Domain.Responses;

namespace Data.Interfaces;

public interface INotificationRepository : IBaseRepository<NotificationEntity>
{
    Task<NotificationResult<Notification>> GetLatestNotificationAsync();
}

