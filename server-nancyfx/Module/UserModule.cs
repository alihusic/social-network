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
    public class UserModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public UserModule():base("/user")
        {
            Get["/"] = _ => "Hello!";
            Post["/authenticate"] = parameters => Authenticate(parameters);
            Get["/register"] = parameters => Register(parameters);
        }
   
        /// <summary>
        /// Method used to handle user authentication/login
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic Register(dynamic parameters)
        {
            //map request to an object

            var registerQuery = new RegisterQuery();
            // TODO:
            //check if username already taken
            // check if data valid
            // save changes to the database
            if (!checkUsername(registerQuery.username))
            {
                if(registerQuery.dateOfBirth is DateTime)
                {
                    saveUser(new UserBuilder()
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
                }
            }
            else
            {
                //usernameTakenException
            }
            // return a status code
            
            return null;
        }

        /// <summary>
        /// saveUSer saves User information to database
        /// </summary>
        /// <param name="name">string. User's name.</param>
        /// <param name="lastName">string. User's last name.</param>
        /// <param name="username">string. User's unique username.</param>
        /// <param name="password">string. User's password.</param>
        /// <param name="country">string. User's country</param>
        /// <param name="city">string. User's city.</param>
        /// <param name="pictureURL">string. User's picture URL.</param>
        /// <param name="gender">string. User's gender.</param>
        /// <param name="dateOfBirth">DateTime. User's birth date.</param>

        /*private void saveUser(string name, string lastName, string username, string password, string country, string city, string pictureURL, string gender, DateTime dateOfBirth)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var user = new User()
                {
                    name = name,
                    lastName = lastName,
                    username = username,
                    password = password,
                    country = country,
                    city = city,
                    pictureURL = pictureURL,
                    gender = gender,
                    dateOfBirth = dateOfBirth
                };

                context.users.Add(user);
                context.SaveChanges();
            }
        }*/


        private void saveUser(User user)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// checkUsername checks if username is unique.
        /// </summary>
        /// <param name="username">string. Users's username.</param>
        /// <returns>
        /// Retruns true if username already exists, else returns false.</returns>

        private bool checkUsername(string username)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.users.Any(u => u.username == username);
            }
        }

    }
    
   
}