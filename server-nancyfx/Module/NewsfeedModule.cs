using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Newsfeed-related requests.
    /// </summary>
    public class NewsfeedModule : NancyModule
    {
        

        /// <summary>
        /// Constructor with route mapping
        /// </summary>
        public NewsfeedModule():base("/newsfeed")
        {
            Get["/"] = _ => "Hello!";
            Post["/load"] = parameters => Load(parameters);
        }

        /// <summary>
        /// Method used to handle newsfeed load request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List of posts</returns>    
        public dynamic Load(dynamic parameters)
        {
            //map request to objects
            var loadQuery = this.Bind<LoadNewsfeedRequest>();
            
            // check user token
            if (!TokenFactory.checkToken(loadQuery.userToken)) throw new Exception("Not logged in");

            //extract from database
            List<Post> recentPosts = PostController.getRecentPosts(loadQuery.interval, loadQuery.userToken.userId);
            
            
            //return ""+recentPosts.Count();
            if (recentPosts.Count() == 0)
            {
                return null;
            }

            //return model
            return JsonConvert.SerializeObject(recentPosts,
                             Newtonsoft.Json.Formatting.None,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });
        }

        
    }

    
}