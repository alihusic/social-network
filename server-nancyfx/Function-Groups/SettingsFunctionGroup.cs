using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    public class SettingsFunctionGroup
    {
        //method used to handle the action of editing user information
        public dynamic EditInfo(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * check if new info is valid
             * save changes to the database
             * return status code
             */
            return null;
        }

        //method used to handle the action of changing the profile picture
        public dynamic ChangeProfilePicture(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * get new url THIS WILL BE HEAVILY MODIFIED
             * save changes to the database
             * return status code
             */
            return null;
        }
        
        //method used to handle the action of changing the password
        public dynamic ChangePassword(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * check hash/password
             * save the new password to the database
             * remove the cookie
             * return status code
             */ 
            return null;
        }
    }
}