using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    public class NewsfeedModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public NewsfeedModule():base("/newsfeed")
        {
            Get["/"] = _ => "Hello!";
            Get["/load"] = parameters => Load(parameters);
        }

        /// <summary>
        /// Method used to handle newsfeed load request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List of posts</returns>
        
        public dynamic Load(dynamic parameters)
        {
            //map request to objects
            var loadQuery = this.Bind<LoadNewsfeedQuery>();

            // check user token
            if (!helpers.checkToken(loadQuery.userToken)) throw new Exception("Not logged in");

            //extract from database
            List<Posts> recentPosts = helpers.getRecentPosts(loadQuery.interval, loadQuery.userToken.userId);

            //check if list empty
            if (!recentPosts.Any()) throw new Exception("No more posts");
            
            //return model
            return Negotiate.WithModel(recentPosts);
        }

    }

    class LoadNewsfeedQuery
    {
        public Token userToken { get; set; }
        public int interval { get; set; }
    }
}