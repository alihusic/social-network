using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    //class used to encapsulate classes with important mapped methods
    public class HandlerFunctionGroup
    {
        public HandlerFunctionGroup()
        {
            chatHandler = new ChatFunctionGroup();
            newsfeedHandler = new NewsfeedFunctionGroup();
            postHandler = new PostFunctionGroup();
            settingsHandler = new SettingsFunctionGroup();
            userHandler = new UserFunctionGroup();
        }
        public ChatFunctionGroup chatHandler {get;}
        public NewsfeedFunctionGroup newsfeedHandler { get; }
        public PostFunctionGroup postHandler { get; }
        public SettingsFunctionGroup settingsHandler { get; }
        public UserFunctionGroup userHandler { get; }
    }

    //consider adding a FunctionGroup class to be extended
}