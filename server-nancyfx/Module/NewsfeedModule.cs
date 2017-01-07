using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using SocialNetwork2;
using SocialNetwork2.Model;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer;
using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkServerNV1.Response;

namespace SocialNetwork2
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Newsfeed-related requests.
    /// Class created by Ermin & Ali.
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
            if (!TokenFactory.checkToken(loadQuery.userToken))
            {
                return new ErrorResponse("You must log in first.");
            }

            //extract from database
            List<Post> recentPosts = PostController.getRecentPosts(loadQuery.interval, loadQuery.userToken.userId);
            
            
            //return ""+recentPosts.Count();
            if (recentPosts.Count() == 0)
            {
                return new ErrorResponse("No more posts.");
            }

            //return model

            PostListResponse responseObject = new PostListResponse(recentPosts);

            /// This is a lazy way to prevent the serialization chaos caused by lazy loading
            return JsonConvert.SerializeObject(responseObject,
                             Newtonsoft.Json.Formatting.None,
                             new JsonSerializerSettings
                             {
                                 NullValueHandling = NullValueHandling.Ignore
                             });
        }

        
    }

    
}