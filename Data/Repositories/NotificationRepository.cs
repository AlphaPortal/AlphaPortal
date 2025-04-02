using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity>(context), INotificationRepository
{
    public async Task<NotificationResult<Notification>> GetLatestNotificationAsync()
    {
        var entity = await _db.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        if (entity == null)
        {
            return new NotificationResult<Notification> { Succeeded = false, StatusCode = 400, Error = "Something went wrong." };
        }

        var notification = entity.MapTo<Notification>();
        return new NotificationResult<Notification> { Succeeded = true, StatusCode = 200, Result = notification }; 
    }
}

