using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    public class UserFunctionGroup
    {

        public FriendsFunctionGroup friends { get; }

        public UserFunctionGroup()
        {
            friends = new FriendsFunctionGroup();
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