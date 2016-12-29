using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer;
using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;

namespace SocialNetwork2.Module
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Notification-related requests.
    /// </summary>
    public class NotificationModule : NancyModule
    {
        
        

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
            
            List<Notification> notifications = NotificationsController.loadNotificationsUser(query.userToken.userId);
            notifications.AddRange(NotificationsController.loadPostNotifications(query.userToken.userId));

            return Negotiate.WithModel(notifications);

        }

    }
}
