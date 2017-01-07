using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using SocialNetworkServerNV1.Response;
using System;

namespace SocialNetwork2
{
    public class SettingsModule : NancyModule
    {
        /// <summary>
        /// Class inheriting NancyModule class.
        /// Used to handle Chat-related requests.
        /// Class created by Ermin & Ali.
        /// </summary>


        /// <summary>
        /// Constructor with route mapping
        /// </summary>
        public SettingsModule():base("/settings")
        {
            Get["/"] = _ => "Hello!";
            Post["/edit_info"] = parameters => EditInfo(parameters);
            Post["/change_password"] = parameters => ChangePassword(parameters);
            Post["/change_profile_picture"] = parameters => ChangeProfilePicture(parameters);
        }

        /// <summary>
        /// Method used to handle the action of editing user information
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status code</returns>
        public dynamic EditInfo(dynamic parameters)
        {
            var editInfoQuery = this.Bind<EditUserInfoRequest>();

            // TODO:
            // check user token
            if (!TokenFactory.checkToken(editInfoQuery.userToken))
                return new ErrorResponse("You must log in first.");

            // check if new info is valid TODO
            // save changes to the database
            if (UtilityController.checkURL(editInfoQuery.pictureURL) && UtilityController.checkURL(editInfoQuery.coverPictureURL))
            { 
      
                SettingsController.editUserInfo(new ProfileInfoBuilder()
                        .Name(editInfoQuery.name)
                        .LastName(editInfoQuery.lastName)
                        .Username(editInfoQuery.username)
                        .Country(editInfoQuery.country)
                        .City(editInfoQuery.city)
                        .PictureURL(editInfoQuery.pictureURL)
                        .CoverPictureURL(editInfoQuery.coverPictureURL)
                        .Gender(editInfoQuery.gender)
                        .DateOfBirth(editInfoQuery.dateOfBirth)
                        .Build(), editInfoQuery.userToken.userId);
            }
            else
            {
                return new ErrorResponse("Invalid profile picture URL.");
            }

            // return status code
            return new MessageResponse("User info successfully updated.");
        }

        /// <summary>
        /// Method used to handle the action of changing the profile picture
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status code</returns>
        public dynamic ChangeProfilePicture(dynamic parameters)
        {
            var changeProfilePictureQuery = this.Bind<ChangePictureRequest>();

            // check user token
            if (!TokenFactory.checkToken(changeProfilePictureQuery.userToken))
                return new ErrorResponse("You must log in first.");

            // get new url THIS WILL BE HEAVILY MODIFIED Allaha mi
            // save changes to the database

            if (UtilityController.checkURL(changeProfilePictureQuery.pictureURL))
            {
                SettingsController.updateProfilePicture(changeProfilePictureQuery.userToken.userId, changeProfilePictureQuery.pictureURL);
            }
            else
            {
                return new ErrorResponse("Invalid picture URL.");
            }
            // return status code

            return new MessageResponse("User profile picture successfully updated.");
        }

        /// <summary>
        /// Method used to handle the action of changing the password
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status code</returns>
        public dynamic ChangePassword(dynamic parameters)
        {
            var changePasswordQuery = this.Bind<ChangePasswordRequest>();

            // check user token
            if (!TokenFactory.checkToken(changePasswordQuery.userToken))
                return new ErrorResponse("You must log in first.");

            // check hash/password
            if (UserController.checkPassword(changePasswordQuery.oldPassword, changePasswordQuery.userToken.userId))
            {
                // save the new password to the database
                SettingsController.updatePassword(changePasswordQuery.newPassword, changePasswordQuery.userToken.userId);
            }
            else
            {
                return new ErrorResponse("Invalid password.");
            }

            
            // brisanje iz liste na serveru
            TokenFactory.removeToken(changePasswordQuery.userToken);

            // brisanje iz baze
            TokenFactory.removeTokenDB(changePasswordQuery.userToken);


            // return status code
            return new MessageResponse("Password successfully updated.");
        }

        /// <summary>
        /// Method used to handle the action of changing the cover picture
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status code</returns>
        public dynamic ChangeCoverPicture(dynamic parameters)
        {
            var changeCoverPictureQuery = this.Bind<ChangePictureRequest>();

            // TODO:
            // check user cookie
            if (!TokenFactory.checkToken(changeCoverPictureQuery.userToken))
                return new ErrorResponse("You must log in first.");

            // get new url THIS WILL BE HEAVILY MODIFIED
            // save changes to the database

            if (UtilityController.checkURL(changeCoverPictureQuery.pictureURL))
            {
                SettingsController.updateCoverPicture(changeCoverPictureQuery.userToken.userId, changeCoverPictureQuery.pictureURL);
            }
            else
            {
                return new ErrorResponse("Invalid picture URL.");
            }
            // return status code

            return new MessageResponse("Cover picture successfully updated.");
        }
    }
}