using SocialNetwork2;
using SocialNetwork2.Model;
using SocialNetwork2.Model.Builder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as controller for post related operations and queries.
    /// Class created by Ermin & Ali.
    /// </summary>
    public static class PostController
    {
        /// <summary>
        /// Method used to retrieve post by specific ID
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <returns>
        /// Object of type Posts</returns>
        public static Post getPost(int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.posts.Find(postId);
                //return (Posts)context.posts.Where(p => p.postCreationDate == postCreationDate);
            }
        }

        /// <summary>
        /// Method used to check if post exists
        /// </summary>
        /// <param name="postId">int. User's Id </param>
        /// <returns>
        /// Returns true if post exists, otherwise returns false</returns>
        public static bool postExists(int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.posts.Any(p => p.postId == postId);
            }
        }

        /// <summary>
        /// Method is used to check if user can see post 
        /// </summary>
        /// <param name="userID">int. User's Id</param>
        /// <returns>
        /// Returns true if user is in friend list, else returns false</returns>
        public static bool isPostVisible(int creatorId, int targetId)
        {
            //ovdje bi mu trebao baciti inheritance. treba ubaciti u Function group metodu iz FriendsFunctionGroup-a getAllFriendsId. Ona treba vratiti listu prijatelja jednog usera.
            List<int> friends = FriendsController.getAllFriendsId(targetId);

            return creatorId == targetId || friends.Contains(creatorId);
        }

        /// <summary>
        /// Method used to check if user liked certain post
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        /// <returns>
        /// Returns true if user liked, else returns false</returns>
        public static bool isLiked(int userId, int postId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                return context.likes.Any(l => l.postId == postId || l.userId == userId);
            }
        }

        /// <summary>
        /// Method used to add new like to the table Likes and increments field numOfLikes in table Posts
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        public static void addLike(Like like)
        {
            using (var context = new SocialNetworkDBContext())
            {


                var post = context.posts.Find(like.postId);
                post.numOfLikes++;
                context.likes.Add(like);
                context.posts.Attach(post);
                context.Entry(post).Property(p => p.numOfLikes).IsModified = true;

                context.notifications.Add(new NotificationBuilder()
                    .CreatorId(like.userId)
                    .EntityTargetId(like.postId)
                    .NotificationType(3)
                    .Build());

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Method used to insert a comment into the database
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="postId">Post ID</param>
        /// <param name="commentText">Textual content of the comment</param>
        public static void addComment(Comment comment)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.notifications.Add(new NotificationBuilder()
                    .CreatorId(comment.userId)
                    .EntityTargetId(comment.postId)
                    .NotificationType(4)
                    .Build());
                context.SaveChanges();

                context.comments.Add(comment);

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }
                } while (saveFailed);
            }
        }

        /// <summary>
        /// Method used to create new post
        /// </summary>
        /// <param name="creatorId">int. Creator's id</param>
        /// <param name="targetId">int. Where is post being posted.</param>
        /// <param name="postContent">string. Contents of a post</param>
        public static void createPost(Post post)
        {
            using (var context = new SocialNetworkDBContext())
            {
                context.notifications.Add(new NotificationBuilder()
                    .CreatorId(post.creatorId)
                    .EntityTargetId(post.postId)
                    .NotificationType(5)
                    .Build());

                context.posts.Add(post);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Method is used to retrieve recent posts that user will see on newsfeed
        /// </summary>
        /// <param name="interval">int. Interval of posts</param>
        /// <param name="userId">int. User's Id</param>
        /// <returns>
        /// Returns List<Posts></returns>
        public static List<Post> getRecentPosts(int interval, int userId)
        {
            List<int> postsId = getRecentPostsId(userId);
            List<Post> posts = new List<Post>();

            using (var context = new SocialNetworkDBContext())
            {
                foreach (var id in postsId)
                {
                    //extract object by ID
                    posts.Add(context.posts.Find(id));
                }

            }

            IEnumerable<Post> postsToReturn = posts.Skip(interval).Take(10);


            return postsToReturn.ToList();

        }

        /// <summary>
        /// Method used to retrieve Id's of recent posts
        /// </summary>
        /// <param name="userId">int. User's Id.</param>
        /// <returns>
        /// Returns List<int></returns>
        public static List<int> getRecentPostsId(int userId)
        {
            List<int> postId = new List<int>();

            List<int> friends = FriendsController.getAllFriendsId(userId);
            friends.Add(userId);

            using (var context = new SocialNetworkDBContext())
            {

                var posts = context.posts;

                foreach (var p in posts)
                {
                    if (friends.Contains(p.creatorId) || friends.Contains(p.creatorId))
                    {
                        postId.Add(p.postId);
                    }
                }


            }
            return postId;
        }

        public static List<Comment> getCommentList(int postId)
        {
            List<Comment> commentList = new List<Comment>();
            using (var context = new SocialNetworkDBContext())
            {
                commentList = context.comments.Where(c => (c.postId==postId)).ToList();
            }
            return commentList;
        }
    }
}