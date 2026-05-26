namespace BlogApp.Mappers;

using BlogApp.DTOs;
using BlogApp.Entities;

public class NotificationMapper
{
    /// <summary>
    ///   Map Notification Entity to Notification DTO
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public static NotificationDTO ToNotificationDTO(Notification notification)
    {
        return new NotificationDTO
        {
            Content = notification.Content,
            TargetType = notification.TargetType,
            TargetId = notification.TargetId,
            CreatedAt = notification.CreatedAt,
            ReceiverId = notification.ReceiverId
        };
    }

    /// <summary>
    ///     Map Notification DTO to Notification Entity
    /// </summary>
    /// <param name="notificationDTO"></param>
    /// <returns></returns>
    public static Notification ToNotification(NotificationDTO notificationDTO)
    {
        return new Notification
        {
            Content = notificationDTO.Content,
            TargetType = notificationDTO.TargetType,
            TargetId = notificationDTO.TargetId,
            ReceiverId = notificationDTO.ReceiverId
        };
    }

    /// <summary>
    ///    Map List of Notification Entity to List of Notification DTO
    /// </summary>
    /// <param name="notifications"></param>
    /// <returns></returns>
    public static List<NotificationDTO> ToNotificationDTOList(List<Notification> notifications)
    {
        return notifications.Select(n => ToNotificationDTO(n)).ToList();
    }
}