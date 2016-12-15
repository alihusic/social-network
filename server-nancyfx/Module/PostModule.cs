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
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Post-related requests.
    /// </summary>
    public class PostModule : NancyModule
    {
        

        /// <summary>
        /// Constructor with route mapping
        /// </summary>

        public PostModule():base("/post")
        {
            Post["/create"] = parameters => Create(parameters);
            Post["/like"] = parameters => Like(parameters);
            Post["/comment"] = parameters => Comment(parameters);
            Get["/"] = _ => "Hello, this is doge";
        }

        /// <summary>
        /// Method used to handle the action of liking a post
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Response</returns>
        public dynamic Like(dynamic parameters)
        {
            //bind request to object
            var likeQuery = this.Bind<PostLikeRequest>();

            //check user token
            if (!TokenFactory.checkToken(likeQuery.userToken))
                throw new Exception("Not logged in.");

            //check if post exists
            if (!PostController.postExists(likeQuery.postId))
                throw new Exception("Post does not exist.");

            // check if post visible to user
            if (!PostController.isPostVisible(likeQuery.creatorId, likeQuery.targetId))
                throw new Exception("Post not visible");

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if (!PostController.isLiked(likeQuery.userId, likeQuery.postId))
            {
                PostController.addLike(new LikesBuilder()
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
            var commentQuery = this.Bind<CommentCreateRequest>();

            //check user token
            if (!TokenFactory.checkToken(commentQuery.userToken))
                throw new Exception("Not logged in.");

            //checking the existance of the post by ID
            if (PostController.postExists(commentQuery.postId))
            {
                //here I am nesting if statements because it is probably easier to handle exceptions. this can be done ofc in one if.
                if (PostController.isPostVisible(commentQuery.userToken.userId, commentQuery.targetId))
                {
                    PostController.addComment(new CommentsBuilder()
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
            var createQuery = this.Bind<PostCreateRequest>();

            //check user token
            if (!TokenFactory.checkToken(createQuery.userToken))
                throw new Exception("Not logged in.");

            //check where is post(on users profile or on another wall)
            try
            {
                if (PostController.isPostVisible(createQuery.userToken.userId, createQuery.targetId))
                {
                    PostController.createPost(new PostsBuilder()
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
            var loadQuery = this.Bind<PostLoadRequest>();

            // checking user token
            if (!TokenFactory.checkToken(loadQuery.userToken))
                throw new Exception("Not logged in.");

            // check if the user has the privileges to see the post
            if (PostController.isPostVisible(loadQuery.creatorId, loadQuery.targetId))
            {
                // extract the post from the database and return response
                return Negotiate.WithModel(PostController.getPost(loadQuery.postId));
            }
            else
            {
                throw new Exception("Post not visible.");
            }
            
        }

    }

    //todo: improve Queries, put them in one file, make them inherit from an ancestor class

    
}