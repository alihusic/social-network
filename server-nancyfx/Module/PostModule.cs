using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
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

            //check user token
            if (!helpers.checkToken(likeQuery.userToken))
                throw new Exception("Not logged in.");

            //check if post exists
            if (!helpers.postExists(likeQuery.postId))
                throw new Exception("Post does not exist.");

            // check if post visible to user
            if (!helpers.isPostVisible(likeQuery.creatorId, likeQuery.targetId))
                throw new Exception("Post not visible");

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if (!helpers.isLiked(likeQuery.userId, likeQuery.postId))
            {
                helpers.addLike(new LikesBuilder()
                    .PostId(likeQuery.postId)
                    .UserId(likeQuery.userToken.userId)
                    .Build());
            }
            else
            {
                throw new Exception("Post already liked.");
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

            //check user token
            if (!helpers.checkToken(commentQuery.userToken))
                throw new Exception("Not logged in.");

            //checking the existance of the post by ID
            if (helpers.postExists(commentQuery.postId))
            {
                //here I am nesting if statements because it is probably easier to handle exceptions. this can be done ofc in one if.
                if (helpers.isPostVisible(commentQuery.userToken.userId, commentQuery.targetId))
                {
                    helpers.addComment(new CommentsBuilder()
                        .CommentText(commentQuery.commentText)
                        .PostId(commentQuery.postId)
                        .UserId(commentQuery.userToken.userId)
                        .Build());
                }
                else
                {
                    throw new Exception("Post not visible.");
                }
            }
            else
            {
                throw new Exception("Post does not exist.");
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

            //check user token
            if (!helpers.checkToken(createQuery.userToken))
                throw new Exception("Not logged in.");

            //check where is post(on users profile or on another wall)
            try
            {
                if (helpers.isPostVisible(createQuery.userToken.userId, createQuery.targetId))
                {
                    helpers.createPost(new PostsBuilder()
                        .CreatorId(createQuery.userToken.userId)
                        .PostContent(createQuery.postContent)
                        .PostCreationDate(DateTime.Now)
                        .TargetId(createQuery.targetId)
                        .NumOfLikes(0)
                        .Build());
                }
                else
                {
                    throw new Exception("Target user not in friend list.");
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
            if (!helpers.checkToken(loadQuery.userToken))
                throw new Exception("Not logged in.");

            // check if the user has the privileges to see the post
            if (helpers.isPostVisible(loadQuery.creatorId, loadQuery.targetId))
            {
                // extract the post from the database and return response
                return Negotiate.WithModel(helpers.getPost(loadQuery.postCreationDate));
            }
            else
            {
                throw new Exception("Post not visible.");
            }
            
        }

    }

    //todo: improve Queries, put them in one file, make them inherit from an ancestor class

    
}