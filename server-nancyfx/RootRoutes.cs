using Nancy;

namespace SocialNetworkServerNV1
{
    public class RootRoutes : NancyModule
    {
        //handlers - a group of handler functions enforcing encapsulation to improve reusability and readability


        private HandlerFunctionGroup handlers = new HandlerFunctionGroup();
        
        //method used to force the module to pack the received object into a model for response 
        public dynamic returnNegotiate(object obj)
        {
            return Negotiate.WithModel(obj);
        }

        /** private void mapRoutes() -> called by the constructor to map the request routes to dynamic functions used to handle them to make code more readable
         */
        private void mapRoutes()
        {

            //easter egg
            Get["/"] = _ => "hail, crawler";

            //Chat handling routes
            Get["/chat/{chat_id}/send"] = parameters => returnNegotiate(handlers.chatHandler.Send(parameters));
            Get["/chat/new_messages"] = parameters => returnNegotiate(handlers.chatHandler.CheckNewMessages(parameters));

            //Newsfeed handling routes
            Get["/newsfeed/load"] = parameters => returnNegotiate(handlers.newsfeedHandler.Load(parameters));

            //User handling routes
            Get["user/authenticate"] = parameters => returnNegotiate(handlers.userHandler.Authenticate(parameters));
            Get["user/register"] = parameters => returnNegotiate(handlers.userHandler.Register(parameters));

            //User friends handling routes
            Get["user/friends/add"] = parameters => returnNegotiate(handlers.userHandler.friends.Add(parameters));
            Get["user/friends/confirm"] = parameters => returnNegotiate(handlers.userHandler.friends.Confirm(parameters));
            Get["user/friends/delete"] = parameters => returnNegotiate(handlers.userHandler.friends.Delete(parameters));
            Get["user/friends/get_all"] = parameters => returnNegotiate(handlers.userHandler.friends.GetAll(parameters));

            //Post handling routes
            Get["post/create"] = parameters => returnNegotiate(handlers.postHandler.Create(parameters));
            Get["post/{post_id}/like"] = parameters => returnNegotiate(handlers.postHandler.Like(parameters));
            Get["post/{post_id}/comment"] = parameters => returnNegotiate(handlers.postHandler.Comment(parameters));
            Get["post/{post_id}"] = parameters => returnNegotiate(handlers.postHandler.Load(parameters));

            //Settings handling routes
            Get["settings/edit_info"] = parameters => returnNegotiate(handlers.settingsHandler.EditInfo(parameters));
            Get["settings/change_password"] = parameters => returnNegotiate(handlers.settingsHandler.ChangePassword(parameters));
            Get["settings/change_profile_picture"] = parameters => returnNegotiate(handlers.settingsHandler.ChangeProfilePicture(parameters));
        }
        public RootRoutes()
        {
            mapRoutes();
        }
    }
}