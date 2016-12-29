using Microsoft.EntityFrameworkCore;
using SocialNetwork2;
using SocialNetworkServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for notification related operations and queries.
    /// </summary>
    public static class NotificationsController
    {
        /// <summary>
        /// Method is used to retrieve all notifications for one user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Returns List<Notifications></returns>
        public static List<Notification> loadNotificationsUser(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.notifications.Where(n => (n.entityTargetId == userId && n.notificationType != 4)).ToList();
            }

        }

        /// <summary>
        /// Method used to retrieve post notifications.
        /// </summary>
        /// <param name="userId">int. User's id.</param>
        /// <returns>
        /// List<Notifications></returns>
        public static List<Notification> loadPostNotifications(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.notifications.Where(n => (context.posts.Find(n.entityTargetId).creatorId == userId && n.notificationType == 4)).ToList();
            }
        }
    }

    
}