using SocialNetwork2.Model;
using System.Collections.Generic;

namespace SocialNetwork2.Response
{
    public class NotificationListResponse:StatusResponse
    {
        public List<Notification> notificationList { get; set; }
        public NotificationListResponse(List<Notification> notificationList) : base(true)
        {
            this.notificationList = notificationList;
        }
    }
}