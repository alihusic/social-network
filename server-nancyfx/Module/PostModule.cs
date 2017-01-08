using Nancy;
using Nancy.ModelBinding;
using SocialNetwork2;
using SocialNetwork2.Model;
using SocialNetwork2.Controller;
using SocialNetwork2.Factory;
using SocialNetwork2.Request;
using SocialNetworkServer;
using SocialNetworkServer.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkServerNV1.Response;

namespace SocialNetwork2
{
    /// <summary>
    /// Class inheriting NancyModule class.
    /// Used to handle Post-related requests.
    /// Class created by Ermin & Ali.
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
            Post["/load"] = parameters => Load(parameters);
            Post["/load_comments"] = parameters => LoadComments(parameters);
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
                return new ErrorResponse("You must log in first.");

            //check if post exists
            if (!PostController.postExists(likeQuery.postId))
                return new ErrorResponse("This post does not exist.");

            // check if post visible to user
            if (!PostController.isPostVisible(likeQuery.creatorId, likeQuery.targetId))
                return new ErrorResponse("This post is not visible to you.");

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if (!PostController.isLiked(likeQuery.userId, likeQuery.postId))
            {
                PostController.addLike(new LikeBuilder()
                    .PostId(likeQuery.postId)
                    .UserId(likeQuery.userToken.userId)
                    .Build());
            }
            else
            {
                return new ErrorResponse("This post is already liked.");
                //mozda disable button na loadu posta, ili staviti na buttonu kada se klikne nek se disable-a
            }

            /* return status code
            */
            return new MessageResponse("Post successfully liked.");
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
                return new ErrorResponse("You must log in first.");

            //checking the existance of the post by ID
            if (PostController.postExists(commentQuery.postId))
            {
                //here I am nesting if statements because it is probably easier to handle exceptions. this can be done ofc in one if.
                if (PostController.isPostVisible(commentQuery.userToken.userId, commentQuery.targetId))
                {
                    PostController.addComment(new CommentBuilder()
                        .CommentText(commentQuery.commentText)
                        .PostId(commentQuery.postId)
                        .UserId(commentQuery.userToken.userId)
                        .Build());
                }
                else
                {
                    return new ErrorResponse("This post is not visible.");
                }
            }
            else
            {
                return new ErrorResponse("This post does not exist.");
            }

            //return status code
            return new MessageResponse("Comment successfully created.");
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
                return new ErrorResponse("You must log in first.");

            //check where is post(on users profile or on another wall)
            try
            {
                if (PostController.isPostVisible(createQuery.userToken.userId, createQuery.targetId))
                {
                    PostController.createPost(new PostBuilder()
                        .CreatorId(createQuery.userToken.userId)
                        .PostContent(createQuery.postContent)
                        .PostCreationDate(DateTime.Now)
                        .TargetId(createQuery.targetId)
                        .NumOfLikes(0)
                        .Build());
                }
                else
                {
                    return new ErrorResponse("This user is not in your friend list.");
                    //neka baci exception da nisu prijatelji
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse("Something went horribly wrong on serverside.");
            }

            //return status code
            return new MessageResponse("Post successfully created.");
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
                return new ErrorResponse("You must log in first.");

            // check if the user has the privileges to see the post
            if (PostController.isPostVisible(loadQuery.creatorId, loadQuery.targetId))
            {
                // extract the post from the database and return response
                return new PostResponse(PostController.getPost(loadQuery.postId));
            }
            else
            {
                return new ErrorResponse("This post is not visible to you.");
            }
            
        }

        /// <summary>
        /// Method used to handle the action of loading all comments of a post.
        /// This is separated from post loading due to the buggy behaviour of
        /// lazy loading.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic LoadComments(dynamic parameters)
        {
            //map request to object
            var postLoadRequest = this.Bind<PostLoadRequest>();

            if (!TokenFactory.checkToken(postLoadRequest.userToken))
                return new ErrorResponse("You must log in first.");

            if (PostController.isPostVisible(postLoadRequest.creatorId, postLoadRequest.targetId))
            {
                // extract the post from the database and return response
                return new CommentListResponse(PostController.getCommentList(postLoadRequest.postId));
            }
            else
            {
                return new ErrorResponse("This post is not visible to you.");
            }

        }
    }

    
}