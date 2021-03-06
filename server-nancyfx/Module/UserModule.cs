using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2;
using SocialNetwork2.Model;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using System;
using System.Linq;
using SocialNetwork2.Response;
using SocialNetwork2.Model.Builder;

namespace SocialNetwork2.Module
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Chat-related requests.
    /// Class created by Ermin & Ali.
    /// </summary>
    public class UserModule : NancyModule
    {
        

        /// <summary>
        /// Constructor with route mapping
        /// </summary>

        public UserModule():base("/user")
        {
            Get["/"] = _ => "Hello!";
            Post["/authenticate"] = parameters => Authenticate(parameters);
            Post["/register"] = parameters => Register(parameters);
            Post["/log_out"] = parameters => LogOut(parameters);
            Post["/user_info"] = parameters => LoadUserInfo(parameters);
        }
   
        /// <summary>
        /// Method used to handle user authentication/login
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status</returns>
        public dynamic Authenticate(dynamic parameters)
        {

            //map request to object
            var authenticateQuery = this.Bind<AuthenticateUserRequest>();
            
            //query user by username
            var user = UserController.getUser(authenticateQuery.username);

            //check if user exists
            if (user == null)
                return new ErrorResponse("User account does not exist.");

            //if (!helpers.userExists(authenticateQuery.username)) return false;

            //check password
            if (authenticateQuery.password != user.password)
                return new ErrorResponse("Invalid password.");

            //if token with userid already found return error
            if (TokenFactory.checkTokenByUserId(user.userId))
            {
                Token token = TokenFactory.getTokenByUserId(user.userId);
                TokenFactory.removeTokenDB(token);
                TokenFactory.removeToken(token);
            }
            //return new ErrorResponse("Already logged in.");

            //delete all tokens of this user from the database(if any - this is to ensure)
            //TODO

            //generate a token
            var userToken = TokenFactory.createNewToken(user.userId);

            return new TokenResponse(userToken);
        }


        /// <summary>
        /// Method used to handle user registration/signup
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status</returns>
        public dynamic Register(dynamic parameters)
        {
            //map request to an object

            var registerQuery = this.Bind<RegisterUserRequest>();
            
            //check if username already taken
            // check if data valid
            // save changes to the database
            if (!SettingsController.checkUsername(registerQuery.username))
            {
                if(registerQuery.dateOfBirth is DateTime)
                {
                    UserController.saveUser(new UserBuilder()
                        .Name(registerQuery.name)
                        .LastName(registerQuery.lastName)
                        .Username(registerQuery.username)
                        .Password(registerQuery.password)
                        .Country(registerQuery.country)
                        .City(registerQuery.city)
                        .PictureURL(registerQuery.pictureURL)
                        .CoverPictureURL(registerQuery.coverPictureURL)
                        .Gender(registerQuery.gender)
                        .DateOfBirth(registerQuery.dateOfBirth)
                        .Build());
                }
                else
                {
                    //typeMissmatchException
                    return new ErrorResponse("Invalid registration data.");
                }
            }
            else
            {
                return new ErrorResponse("This username is already taken.");
            }
            // return a status code

            return new MessageResponse("User account successfully created.");
        }

        /// <summary>
        /// MEthod used to log out a User
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>Status.</returns>
        public dynamic LogOut(dynamic parameters)
        {
            //binding data
            var logOutQuery = this.Bind<ConfidentialRequest>();

            //checking token
            if (!TokenFactory.checkToken(logOutQuery.userToken))
                return new ErrorResponse("You must log in first.");


            //deleting token (logging out user) - on client side user should be redirected to log in page
            TokenFactory.removeTokenDB(logOutQuery.userToken);
            TokenFactory.removeToken(logOutQuery.userToken);


            return new MessageResponse("User successfully logged out.");

        }

        /// <summary>
        /// MEthod used to load user information
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>Status.</returns>
        public dynamic LoadUserInfo(dynamic parameters)
        {
            var loadUserInfoQuery = this.Bind<ConfidentialRequest>();


            if (!TokenFactory.checkToken(loadUserInfoQuery.userToken))
                return new ErrorResponse("You must log in first.");

            var userInfo = UserController.getUserProfileInfo(loadUserInfoQuery.userToken.userId);

            return new ProfileInfoResponse(userInfo);
        }

    }
    
   
}