using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
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
            var user = getUser(authenticateQuery.username);
            
            //check if user exists
            if (user == null) return false;

            //if (!helpers.userExists(authenticateQuery.username)) return false;

            //check password
            if (authenticateQuery.password != user.password) return false;

            //if token with userid already found return error
            if (helpers.checkTokenByUserId(user.userId)) return false;

            //delete all tokens of this user from the database(if any - this is to ensure)
            //TODO

            //generate a token
            var userToken = FunctionGroup.createNewToken(user.userId);

            return Negotiate.WithModel(userToken);
        }

        /// <summary>
        /// Method used to return user object by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Corresponding user object</returns>
        public User getUser(string username)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return (User)context.users.Where(p => p.username == username);
            }
        }

        /// <summary>
        /// Method used to handle user registration/signup
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic Register(dynamic parameters)
        {
            /* TODO:
             * check if username already taken
             * check if data valid
             * save changes to the database
             * return a status code
             */
            return null;
        }
    }
    
    public class AuthenticateQuery
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}