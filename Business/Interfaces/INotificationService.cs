using Business.Models;
using Domain.Models;
using Domain.Responses;

namespace Business.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResult> AddNotificationAsync(NotificationItemForm form);
        Task DismissNotificationAsync(string notificationId, string userId);
        Task<NotificationResult<IEnumerable<Notification>>> GetNotificationASync(string userId, string? roleName = null, int take = 10);
    }
}