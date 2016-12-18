﻿using Nancy;
using Nancy.ModelBinding;
using SocialNetworkServer;
using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;

namespace SocialNetworkServerNV1.Module
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Notification-related requests.
    /// </summary>
    public class NotificationModule : NancyModule
    {
        /// Notification type 1 - Person has sent you a friend request
        /// Notification type 2 - Person has confirmed your friend request
        /// Notification type 3 - Person has liked your post
        /// Notification type 4 - Person has commented your post
        /// Notification type 5 - Person has posted on your wall
        

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
            if (!TokenFactory.checkToken(query.userToken)) throw new Exception("Not logged in");

            // load notifications from database
            
            List<Notifications> notifications = NotificationsController.loadNotificationsUser(query.userToken.userId);
            notifications.AddRange(NotificationsController.loadPostNotifications(query.userToken.userId));

            return Negotiate.WithModel(notifications);

        }

    }
}
