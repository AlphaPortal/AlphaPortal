using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class NotificationTypeRepository(DataContext context) : BaseRepository<NotificationTypeEntity>(context), INotificationTypeRepository
{
}

