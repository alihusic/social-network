using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkServerNV1
{
    public class PostModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public PostModule():base("/post")
        {
            //change some of these to post requests
            Get["/create"] = parameters => Create(parameters);
            Get["/like"] = parameters => Like(parameters);
            Get["/comment"] = parameters => Comment(parameters);
            Get["/"] = parameters => Load(parameters);
        }

        /// <summary>
        /// Method used to handle the action of liking a post
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response</returns>
        public dynamic Like(dynamic parameters)
        {
            //bind request to object
            var likeQuery = this.Bind<LikeQuery>();

            //check user cookie
            if (!helpers.checkToken(likeQuery.userToken)) return false;

            //check if post exists
            if (!helpers.postExists(likeQuery.postId)) return false;

            // check if post visible to user
            // maybe refactor this later
            if (!helpers.isPostVisible(likeQuery.creatorId, likeQuery.targetId)) return false;

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if (!helpers.isLiked(likeQuery.userId, likeQuery.postId))
            {
                helpers.addLike(likeQuery.userId, likeQuery.postId);
            }
            else
            {
                return false;
                //mozda disable button na loadu posta, ili staviti na buttonu kada se klikne nek se disable-a
            }

            /* return status code
            */
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of commenting on a post
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response</returns>
        public dynamic Comment(dynamic parameters)
        {
            //map request to object
            var commentQuery = this.Bind<CommentQuery>();

            //check user cookie
            if (!helpers.checkToken(commentQuery.userToken))
            {
                return false;
            }

            //checking the existance of the post by ID
            if (helpers.postExists(commentQuery.postId))
            {
                //here I am nesting if statements because it is probably easier to handle exceptions. this can be done ofc in one if.
                if (helpers.isPostVisible(commentQuery.creatorId, commentQuery.targetId))
                {
                    helpers.addComment(commentQuery.userId, commentQuery.postId, commentQuery.commentText);
                }
                else
                {
                    return false;
                    //thorws postNotVisibleException
                }

            }
            else
            {
                return false;
                //thorws postDoenstExist exception
            }

            //return status code
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of creating a new post
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response</returns>
        public dynamic Create(dynamic parameters)
        {
            //bind request to object
            var createQuery = this.Bind<CreateQuery>();

            //check user cookie
            if (!helpers.checkToken(createQuery.userToken)) return false;

            //check where is post(on users profile or on another wall)
            try
            {
                if (helpers.isPostVisible(createQuery.creatorId, createQuery.targetId))
                {
                    helpers.createPost(createQuery.creatorId, createQuery.targetId, createQuery.postContent);
                }
                else
                {
                    return false;
                    //neka baci exception da nisu prijatelji
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            //return status code
            return Negotiate.WithStatusCode(200);
        }

        /// <summary>
        /// Method used to handle the action of loading a post
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response with the post</returns>
        public dynamic Load(dynamic parameters)
        {
            //binding the request to object
            var loadQuery = this.Bind<LoadQuery>();

            // checking user token
            if (!helpers.checkToken(loadQuery.userToken)) return false;

            // check if the user has the privileges to see the post
            if (helpers.isPostVisible(loadQuery.creatorId, loadQuery.targetId))
            {
                // extract the post from the database and return response
                return Negotiate.WithModel(helpers.getPost(loadQuery.postCreationDate));
            }
            else
            {
                return false;
            }
            
        }

    }

    //todo: improve Queries, put them in one file, make them inherit from an ancestor class

}