using Data.Entities;
using Domain.Responses;

namespace Data.Interfaces;

public interface IUserDisMissNotificationRepository : IBaseRepository<UserDismissNotificationEntity>
{
    Task<RepositoryResult<IEnumerable<string>>> GetNotificationsIdAsync(string userId);
}

