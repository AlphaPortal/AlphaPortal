using Business.Models;
using Data.Interfaces;
using Domain.Responses;

namespace Business.Services;

public class NotificationService(INotificationRepository notificationRepository, INotificationTypeRepository notificationTypeRepository, INotificationTargerRepository notificationTargerRepository, IUserDisMissNotificationRepository userDisMissNotificationRepository)
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly INotificationTypeRepository _notificationTypeRepository = notificationTypeRepository;
    private readonly INotificationTargerRepository _notificationTargerRepository = notificationTargerRepository;
    private readonly IUserDisMissNotificationRepository _userDisMissNotificationRepository = userDisMissNotificationRepository;

    public async Task<NotificationResult> AddNotificationAsync(NotificationItemForm form)
    {
        if (form == null)
        {
            return new NotificationResult { Succeeded = false, StatusCode = 400 };
        }

        if (string.IsNullOrEmpty(form.ImageUrl))
        {
            switch (form.NotificationTypeId)
            {
                // User
                case 1:
                    form.ImageUrl = "Images/Profiles/avatar.svg";
                    break;
                    // Project
                case 2:
                    form.ImageUrl = "Images/Projects/project-image.svg";
                    break;
            }
        }


    }
}
