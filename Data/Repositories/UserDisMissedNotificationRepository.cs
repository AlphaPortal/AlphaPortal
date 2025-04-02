using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserDisMissedNotificationRepository(DataContext context) : BaseRepository<UserDismissNotificationEntity>(context), IUserDisMissNotificationRepository
{
    public async Task<RepositoryResult<IEnumerable<string>>> GetNotificationsIdAsync(string userId)
    {
        var ids = await _db.Where(x => x.UserId == userId).Select(x => x.NotificationId).ToListAsync();
        return new RepositoryResult<IEnumerable<string>> { Succeeded = true, StatusCode = 200, Result = ids };
    }
}

