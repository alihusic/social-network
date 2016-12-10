using Nancy;
using Nancy.ModelBinding;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using System;

namespace SocialNetworkServerNV1
{
    public class SettingsModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public SettingsModule():base("/settings")
        {
            Get["/"] = _ => "Hello!";
            Get["settings/edit_info"] = parameters => EditInfo(parameters);
            Get["settings/change_password"] = parameters => ChangePassword(parameters);
            Get["settings/change_profile_picture"] = parameters => ChangeProfilePicture(parameters);
        }

        /// <summary>
        /// Method used to handle the action of editing user information
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Status code</returns>
        public dynamic EditInfo(dynamic parameters)
        {
            var editInfoQuery = this.Bind<EditInfoQuery>();

            // TODO:
            // check user token
            if (!helpers.checkToken(editInfoQuery.userToken))
                throw new Exception("Not logged in.");

            // check if new info is valid TODO
            // save changes to the database
            if (helpers.checkURL(editInfoQuery.pictureURL) && helpers.checkURL(editInfoQuery.coverPictureURL))
            { 
      
                helpers.editUserInfo(new UserBuilder()
                        .Name(editInfoQuery.name)
                        .LastName(editInfoQuery.lastName)
                        .Username(editInfoQuery.username)
                        .Country(editInfoQuery.country)
                        .City(editInfoQuery.city)
                        .PictureURL(editInfoQuery.pictureURL)
                        .CoverPictureURL(editInfoQuery.coverPictureURL)
                        .Gender(editInfoQuery.gender)
                        .DateOfBirth(editInfoQuery.dateOfBirth)
                        .Build());
            }
            else
            {
                throw new Exception("Invalid profile picture URL.");
            }

            // return status code
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of changing the profile picture
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Status code</returns>
        public dynamic ChangeProfilePicture(dynamic parameters)
        {
            var changeProfilePictureQuery = this.Bind<ChangeProfilePictureQuery>();

            // check user token
            if (!helpers.checkToken(changeProfilePictureQuery.userToken)) throw new Exception("Not logged in.");

            // get new url THIS WILL BE HEAVILY MODIFIED Allaha mi
            // save changes to the database

            if (helpers.checkURL(changeProfilePictureQuery.pictureURL))
            {
                helpers.updateProfilePicture(changeProfilePictureQuery.userToken.userId, changeProfilePictureQuery.pictureURL);
            }
            else
            {
                throw new Exception("Invalid URL.");
            }
            // return status code

            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of changing the password
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Status code</returns>
        public dynamic ChangePassword(dynamic parameters)
        {
            var changePasswordQuery = this.Bind<ChangePasswordQuery>();

            // check user token
            if (!helpers.checkToken(changePasswordQuery.userToken)) throw new Exception("Not logged in.");

            // check hash/password
            if (helpers.checkPassword(changePasswordQuery.oldPassword, changePasswordQuery.userToken.userId))
            {
                // save the new password to the database
                helpers.updatePassword(changePasswordQuery.newPassword, changePasswordQuery.userToken.userId);
            }
            else
            {
                throw new Exception("Invalid password.");
            }

            // remove the token -- ovo ces ti Ali morati dovrisiti (Ermin)
            // brisanje iz liste na serveru
            FunctionGroup.removeToken(changePasswordQuery.userToken);

            // brisanje iz baze
            helpers.removeTokenDB(changePasswordQuery.userToken);
            

            // return status code
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of changing the cover picture
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Status code</returns>
        public dynamic ChangeCoverPicture(dynamic parameters)
        {
            var changeCoverPictureQuery = new ChangeCoverPictureQuery();

            // TODO:
            // check user cookie
            if (!helpers.checkToken(changeCoverPictureQuery.userToken)) return false;

            // get new url THIS WILL BE HEAVILY MODIFIED
            // save changes to the database

            if (helpers.checkURL(changeCoverPictureQuery.coverPictureURL))
            {
                helpers.updateCoverPicture(changeCoverPictureQuery.userToken.userId, changeCoverPictureQuery.coverPictureURL);
            }
            else
            {
                return false;
            }
            // return status code

            return Negotiate.WithStatusCode(200);
        }
    }
}