using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Responses;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business.Services;

public class NotificationService(INotificationRepository notificationRepository, INotificationTypeRepository notificationTypeRepository, INotificationTargerRepository notificationTargerRepository, IUserDisMissNotificationRepository userDisMissNotificationRepository, IHubContext<NotificationHub> notificationHub) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository;
    private readonly INotificationTargerRepository _notificationTargerRepository = notificationTargerRepository;
    private readonly IUserDisMissNotificationRepository _userDisMissNotificationRepository = userDisMissNotificationRepository;
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;

    public async Task<NotificationResult> AddNotificationAsync(NotificationItemForm form)
    {
        if (form == null)
        {
            return new NotificationResult { Succeeded = false, StatusCode = 400 };
        }

        if (string.IsNullOrEmpty(form.Image))
        {
            switch (form.NotificationTypeId)
            {
                // User
                case 1:
                    form.Image = "Images/Profiles/avatar.svg";
                    break;
                // Project
                case 2:
                    form.Image = "Images/Projects/project-image.svg";
                    break;
            }
        }

        var notificationEntity = form.MapTo<NotificationEntity>();
        var result = await _notificationRepository.AddAsync(notificationEntity);

        if (result.Succeeded)
        {
            var notificationResult = await _notificationRepository.GetLatestNotificationAsync();
            await _notificationHub.Clients.All.SendAsync("ReceiveNotification", notificationResult.Result);
        }

        return result.Succeeded
            ? new NotificationResult { Succeeded = true, StatusCode = 200 }
            : new NotificationResult { Succeeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<NotificationResult<IEnumerable<Notification>>> GetNotificationASync(string userId, string? roleName = null, int take = 10)
    {
        var adminTargetName = "Admin";
        var dismissedNotificationResult = await _userDisMissNotificationRepository.GetNotificationsIdAsync(userId);
        var dismissedNotificationIds = dismissedNotificationResult.Result;

        var notificationResult = (!string.IsNullOrEmpty(roleName) && roleName == adminTargetName)
            ? await _notificationRepository.GetAllAsync(orderByDescending: true, sortByColumn: x => x.CreateDate,
            filterBy: x => !dismissedNotificationIds!.Contains(x.Id), take: take)

            : await _notificationRepository.GetAllAsync(orderByDescending: true, sortByColumn: x => x.CreateDate,
                filterBy: x => !dismissedNotificationIds!.Contains(x.Id) && x.NotificationTarget.TargetName != adminTargetName, take: take);

        if (!notificationResult.Succeeded)
        {
            return new NotificationResult<IEnumerable<Notification>> { Succeeded = false, StatusCode = 404, Error = notificationResult.Error };
        }

        if (notificationResult.Result != null)
        {
            var notifications = notificationResult.Result.Select(entity => entity.MapTo<Notification>());
            return new NotificationResult<IEnumerable<Notification>> { Succeeded = true, StatusCode = 200, Result = notifications };
        }

        return new NotificationResult<IEnumerable<Notification>> { Succeeded = false, StatusCode = 400, Error = notificationResult.Error };
    }

    public async Task DismissNotificationAsync(string notificationId, string userId)
    {
        var exists = await _userDisMissNotificationRepository.ExistsAsync(x => x.NotificationId == notificationId && x.UserId == userId);
        if (!exists.Succeeded)
        {
            var entity = new UserDismissNotificationEntity
            {
                NotificationId = notificationId,
                UserId = userId,
            };

            await _userDisMissNotificationRepository.AddAsync(entity);
        }

        
        await _notificationHub.Clients.User(userId).SendAsync("NotificationDisMissed", notificationId);
    }
}
