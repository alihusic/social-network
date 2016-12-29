using Microsoft.EntityFrameworkCore;
using SocialNetwork2.Model;
using System.Linq;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for settings related operations and queries.
    /// </summary>
    public static class SettingsController
    {
        /// <summary>
        /// Method used to edit user's info.
        /// </summary>
        /// <param name="userId">int. User's id.</param>
        /// <param name="name">string. User's name.</param>
        /// <param name="lastName">string. User's last name.</param>
        /// <param name="username">string. User's username.</param>
        /// <param name="country">string. User's country.</param>
        /// <param name="city">string. User's city.</param>
        /// <param name="pictureURL">string. User's picture URL.</param>
        /// <param name="gender">string. User's gender.</param>
        /// <param name="dateOfBirth">string. User's date of birth.</param>
        public static void editUserInfo(ProfileInfo request, int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var user = context.users.Where(u => u.userId == userId).FirstOrDefault();

                user.name = request.name;
                user.lastName = request.lastName;
                user.username = request.username;
                user.country = request.country;
                user.city = request.city;
                user.pictureURL = request.pictureURL;
                user.coverPictureURL = request.coverPictureURL;
                user.gender = request.gender;
                user.dateOfBirth = request.dateOfBirth;

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }
                } while (saveFailed);
            }
        }

        /// <summary>
        /// Method used to update User's profile picture.
        /// </summary>
        /// <param name="userId">int. User's ID.</param>
        /// <param name="pictureURL">string. Picture URL.</param>
        public static void updateProfilePicture(int userId, string pictureURL)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var user = context.users.Find(userId);
                user.pictureURL = pictureURL;


                context.users.Attach(user);
                context.Entry(user).Property(u => u.pictureURL).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to set new password.
        /// </summary>
        /// <param name="newPassword">string. New password.</param>
        /// <param name="userId">int. User's ID.</param>
        public static void updatePassword(string newPassword, int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var user = context.users.Find(userId);
                user.password = newPassword;

                context.users.Attach(user);
                context.Entry(user).Property(u => u.password).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to update cover picture.
        /// </summary>
        /// <param name="userId">int. User's id.</param>
        /// <param name="coverPictureURL">string. Cover picture URL.</param>
        public static void updateCoverPicture(int userId, string coverPictureURL)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var user = context.users.Find(userId);
                user.coverPictureURL = coverPictureURL;

                context.users.Attach(user);
                context.Entry(user).Property(u => u.coverPictureURL).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to check if username is unique.
        /// </summary>
        /// <param name="username">string. Users's username.</param>
        /// <returns>
        /// Retruns true if username already exists, else returns false.</returns>
        public static bool checkUsername(string username)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Any(u => u.username == username);
            }
        }

    }
}