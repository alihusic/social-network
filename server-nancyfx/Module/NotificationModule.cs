using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer;
using SocialNetworkServer.Model;
using SocialNetworkServerNV1.Response;
using System;
using System.Collections.Generic;

namespace SocialNetwork2.Module
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Notification-related requests.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class NotificationModule : NancyModule
    {
        ///  Notification type 1 - Person has sent you a friend request
        ///  Notification type 2 - Person has confirmed your friend request
        ///  Notification type 3 - Person has liked your post
        ///  Notification type 4 - Person has commented your post
        ///  Notification type 5 - Person has posted on your wall
        

        /// <summary>
        /// Constructor with route mapping
        /// </summary>
        public NotificationModule() : base("/notification")
        {

            Get["/"] = _ => "Hello!";
            Get["/load"] = parameters => LoadNotifications(parameters);
        }

        /// <summary>
        /// Method used to handle notification load request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List of notifications</returns>
        public dynamic LoadNotifications(dynamic parameters)
        {
            // map request to object
            var query = this.Bind<ConfidentialRequest>();

            // check user token
            if (!TokenFactory.checkToken(query.userToken))
            {
                return new ErrorResponse("You must log in first.");
            }

            // load notifications from database
            
            List<Notification> notifications = NotificationsController.loadNotificationsUser(query.userToken.userId);
            notifications.AddRange(NotificationsController.loadPostNotifications(query.userToken.userId));

            return new NotificationListResponse(notifications);

        }

    }
}
