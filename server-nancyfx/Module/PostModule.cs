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
            if (!postExists(likeQuery.postId)) return false;

            // check if post visible to user
            // maybe refactor this later
            if (!isPostVisible(likeQuery.creatorId, likeQuery.targetId)) return false;

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if (!isLiked(likeQuery.userId, likeQuery.postId))
            {
                addLike(likeQuery.userId, likeQuery.postId);
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
            if (postExists(commentQuery.postId))
            {
                //here I am nesting if statements because it is probably easier to handle exceptions. this can be done ofc in one if.
                if (isPostVisible(commentQuery.creatorId, commentQuery.targetId))
                {
                    addComment(commentQuery.userId, commentQuery.postId, commentQuery.commentText);
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
                if (isPostVisible(createQuery.creatorId, createQuery.targetId))
                {
                    createPost(createQuery.creatorId, createQuery.targetId, createQuery.postContent);
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
            if (isPostVisible(loadQuery.creatorId, loadQuery.targetId))
            {
                // extract the post from the database and return response
                return Negotiate.WithModel(getPost(loadQuery.postCreationDate));
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// @getPost used to retrieve post for a certain creation time
        /// </summary>
        /// <param name="postCreationDate">DateTime. time when post was created</param>
        /// <returns>
        /// Object of type Posts</returns>
        public Posts getPost(DateTime postCreationDate)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return (Posts)context.posts.Where(p => p.postCreationDate == postCreationDate);
            }
        }

        /// <summary>
        /// @postExists checks if post exists
        /// </summary>
        /// <param name="postId">int. User's Id </param>
        /// <returns>
        /// Returns true if post exists, otherwise returns false</returns>
        public bool postExists(int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.posts.Any(p => p.postsId == postId);
            }
        }

        /// <summary>
        /// @isPostVisible checks if user can see post 
        /// </summary>
        /// <param name="userID">int. User's Id</param>
        /// <returns>
        /// Returns true if user is in friend list, else returns false</returns>
        public bool isPostVisible(int creatorId, int targetId)
        {
            //ovdje bi mu trebao baciti inheritance. treba ubaciti u Function group metodu iz FriendsFunctionGroup-a getAllFriendsId. Ona treba vratiti listu prijatelja jednog usera.
            List<int> friends = helpers.getAllFriendsId(targetId);

            return friends.Contains(creatorId);
        }

        /// <summary>
        /// @isLiked checks if user liked certain post
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        /// <returns>
        /// Returns true if user liked, else returns false</returns>
        public bool isLiked(int userId, int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.likes.Any(l => l.postId == postId || l.userId == userId);
            }
        }

        /// <summary>
        /// @addLike adds new like to the table Likes and increments field numOfLikes in table Posts
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        public void addLike(int userId, int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var like = new Likes()
                {
                    postId = postId,
                    userId = userId
                };

                var post = new Posts()
                {
                    postsId = postId,
                    numOfLikes = +1
                };

                context.posts.Attach(post);
                context.Entry(post).Property(p => p.numOfLikes).IsModified = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to insert a comment into the database
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="postId">Post ID</param>
        /// <param name="commentText">Textual content of the comment</param>
        public void addComment(int userId, int postId, string commentText)
        {
            using (var context = new SocialNetworkDBContext())
            {
                Comments comment = new Comments()
                {
                    commentText = commentText,
                    postId = postId,
                    userId = userId
                };

                context.comments.Add(comment);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// @createPost creates new post
        /// </summary>
        /// <param name="creatorId">int. Creator's id</param>
        /// <param name="targetId">int. Where is post being posted.</param>
        /// <param name="postContent">string. Contents of a post</param>
        public void createPost(int creatorId, int targetId, string postContent)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var post = new Posts()
                {
                    creatorId = creatorId,
                    targetId = targetId,
                    postContent = postContent,
                    numOfLikes = 0,
                    postCreationDate = DateTime.Now
                };

                context.posts.Add(post);
                context.SaveChanges();
            }
        }
    }

    //todo: improve Queries, put them in one file, make them inherit from an ancestor class

    class CommentQuery
    {
        public int userId { get; set; }
        public int postId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public string commentText { get; set; }
        public Token userToken { get; set; }
    }

    class CreateQuery
    {
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public Token userToken { get; set; }
        public string postContent { get; set; }
    }

    class LikeQuery
    {
        public int userId { get; set; }
        public int creatorId { get; set; }
        public int targetId { get; set; }
        public int postId { get; set; }
        public Token userToken { get; set; }
    }

    class LoadQuery
    {
        public Token userToken { get; set; }
        public int postId { get; set; }
        public int targetId { get; set; }
        public int creatorId { get; set; }
        public DateTime postCreationDate { get; set; }
    }
}