using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class NotificationTargerRepository(DataContext context) : BaseRepository<NotificationTargetEntity>(context), INotificationTargerRepository
{
}

