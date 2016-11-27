using Nancy;

namespace SocialNetworkServerNV1
{
    public class UserModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public UserModule():base("/user")
        {
            Get["/"] = _ => "Hello!";
            Get["/authenticate"] = parameters => Authenticate(parameters);
            Get["/register"] = parameters => Register(parameters);
        }

        //method used to handle user authentication/login
        public dynamic Authenticate(dynamic parameters)
        {
            /* TODO:
             * check if user exists
             * check password hash
             * generate a cookie
             * return status code
             */
            return null;
        }

        //method used to handle user registration/signup
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
}