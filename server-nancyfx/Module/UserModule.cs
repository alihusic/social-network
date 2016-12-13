using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using System;
using System.Linq;

namespace SocialNetworkServerNV1
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Chat-related requests.
    /// </summary>
    public class UserModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

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
            var authenticateQuery = this.Bind<AuthenticateQuery>();
            
            //query user by username
            var user = helpers.getUser(authenticateQuery.username);

            //check if user exists
            if (user == null) return Negotiate.WithStatusCode(404);

            //if (!helpers.userExists(authenticateQuery.username)) return false;

            //check password
            if (authenticateQuery.password != user.password) throw new System.Exception("Invalid password");

            //if token with userid already found return error
            if (helpers.checkTokenByUserId(user.userId)) throw new System.Exception("Already logged in");

            //delete all tokens of this user from the database(if any - this is to ensure)
            //TODO

            //generate a token
            var userToken = FunctionGroup.createNewToken(user.userId);

            return Negotiate.WithModel(userToken);
        }


        /// <summary>
        /// Method used to handle user registration/signup
        /// </summary>
        /// <param name="parameters">dynamic</param>
        /// <returns>Status</returns>
        public dynamic Register(dynamic parameters)
        {
            //map request to an object

            var registerQuery = this.Bind<RegisterQuery>();
            
            //check if username already taken
            // check if data valid
            // save changes to the database
            if (!helpers.checkUsername(registerQuery.username))
            {
                if(registerQuery.dateOfBirth is DateTime)
                {
                    helpers.saveUser(new UserBuilder()
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
                    throw new Exception("Fail");
                }
            }
            else
            {
                throw new Exception("Username already taken.");
            }
            // return a status code
            
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// MEthod used to log out a User
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>Status.</returns>
        public dynamic LogOut(dynamic parameters)
        {
            //binding data
            var logOutQuery = this.Bind<LogOutQuery>();

            //checking token
            if (!helpers.checkToken(logOutQuery.userToken))
                throw new Exception("Not logged in.");


            //deleting token (logging out user) - on client side user should be redirected to log in page
            helpers.removeTokenDB(logOutQuery.userToken);
            FunctionGroup.removeToken(logOutQuery.userToken);


            return "User logged out!";

        }

        /// <summary>
        /// MEthod used to log out a User
        /// </summary>
        /// <param name="parameters">dynamic.</param>
        /// <returns>Status.</returns>
        public dynamic LoadUserInfo(dynamic parameters)
        {
            var loadUserInfoQuery = this.Bind<LoadUserInfoQuery>();


            if (!helpers.checkToken(loadUserInfoQuery.userToken))
                throw new Exception("Not logged in.");

            var userInfo = helpers.getUserProfileInfo(loadUserInfoQuery.userToken.userId);

            return userInfo;
        }

    }
    
   
}