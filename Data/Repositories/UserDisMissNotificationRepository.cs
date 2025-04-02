using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserDisMissNotificationRepository(DataContext context) : BaseRepository<UserDismissNotificationEntity>(context), IUserDisMissNotificationRepository
{
}

