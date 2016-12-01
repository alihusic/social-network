using Nancy;

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