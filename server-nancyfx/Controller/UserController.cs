using SocialNetwork2.Model;
using SocialNetworkServer.Builder;
using System.Linq;


namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for user related operations and queries.
    /// Class created by Ermin & Ali.
    /// </summary>
    public static class UserController
    {

        /// <summary>
        /// Method used to return user object by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding user object</returns>
        public static User getUser(string username)
        {
            User user = null;
            using (var context = new SocialNetworkDBContext())
            {
                var results = context.users.Where(p => p.username.Equals(username));
                if (results.Any()) return results.FirstOrDefault();
                //return new User { username=username};
            }
            return user;
        }

        /// <summary>
        /// Method used to return user object by user id
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding user object</returns>
        public static User getUserById(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                //var results = context.users.Where(p => p.username.Equals(username));
                //if (results.Any()) return results.FirstOrDefault();
                //return new User { username=username};
                return context.users.Find(userId);
            }
        }

        /// <summary>
        /// Method used to check if there is a user with a certain Id
        /// </summary>
        /// <param name="userId"> Type int. Users id that is sent to the method. </param>
        /// <returns>
        /// Returns boolean. True if user exists, false if doesn't.</returns>
        public static bool userExists(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Any(u => (u.userId == userId));
            }

        }


        /// <summary>
        /// MEthod used to check if password already exists in databse.
        /// </summary>
        /// <param name="password">string. User's password.</param>
        /// <returns>
        /// Returns true if password exists, else returns false.</returns>
        public static bool checkPassword(string password, int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Find(userId).password.Equals(password);
                //return context.users.Any(u => u.password == password);
            }
        }

        /// <summary>
        /// Method used to save user's info
        /// </summary>
        /// <param name="user">USer. User object.</param>
        public static void saveUser(User user)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to retrieve information needed for profile population.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Object of type ProfileInfo.</returns>
        public static ProfileInfo getProfileInfo(int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var tempUser = context.users.Find(userId);
                ProfileInfo profileInfo = new ProfileInfoBuilder()
                    .Username(tempUser.username)
                    .PictureURL(tempUser.pictureURL)
                    .CoverPictureURL(tempUser.pictureURL)
                    .Name(tempUser.name)
                    .LastName(tempUser.lastName)
                    .Gender(tempUser.gender)
                    .DateOfBirth(tempUser.dateOfBirth)
                    .Country(tempUser.country)
                    .City(tempUser.city)
                    .Build();
                return profileInfo;
            }
        }


        /// <summary>
        /// Method used to retrieve user's profile info
        /// </summary>
        /// <param name="userId">int. User's id</param>
        /// <returns>
        /// Object of type ProfileInfo</returns>
        public static ProfileInfo getUserProfileInfo(int userId)
        {
            var user = getUserById(userId);

            return new ProfileInfoBuilder()
                .Name(user.name)
                .LastName(user.lastName)
                .Username(user.username)
                .Country(user.country)
                .City(user.city)
                .PictureURL(user.pictureURL)
                .CoverPictureURL(user.coverPictureURL)
                .Gender(user.gender)
                .DateOfBirth(user.dateOfBirth)
                .Build();
        }

    }
}