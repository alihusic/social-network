using Nancy;
using Nancy.ModelBinding;
using SocialNetworkServer;
using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;

namespace SocialNetworkServerNV1.Module
{
    public class NotificationModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();
        private Dictionary<int, Action> methodDictionary;

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
            var query = this.Bind<NotificationQuery>();

            // check user token
            if (!helpers.checkToken(query.userToken)) throw new Exception("Not logged in");

            // load notifications from database
            List<Notifications> notifications = helpers.loadNotificationsUser(query.userToken.userId);
            notifications.AddRange(helpers.loadPostNotifications(query.userToken.userId));

            return Negotiate.WithModel(notifications);

        }

    }
}
