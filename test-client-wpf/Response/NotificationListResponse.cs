using SocialNetworkServer.Model;
using System.Collections.Generic;

namespace SocialNetworkServerNV1.Response
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