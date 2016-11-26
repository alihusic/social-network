using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork;
using SocialNetwork.Model;
using SocialNetwork.Controller;

namespace SocialNetworkServerNV1
{
    public class PostFunctionGroup : FunctionGroup
    {
        //method used to handle the action of liking a post
        public dynamic Like(dynamic parameters)
        {
            /* TODO:
            check user cookie
            */
            //check if post exists
            postExists(parameters.postId);

            // check if post visible to user
            
            isPostVisible(parameters.creatorId, parameters.targetId);

            /* check if user already liked -> remove like
            *                       else -> add like(suggestion: we can disable like button on post load if user has already liked smth (Ermin))*/
            if(!isLiked(parameters.userId, parameters.postId))
            {
                addLike(parameters.userId, parameters.postId);
            }
            else
            {
                //mozda disable button na loadu posta, ili staviti na buttonu kada se klikne nek se disable-a
            }

            /* return status code
            */
            return null;
        }

        //method used to handle the action of commenting on a post
        public dynamic Comment(dynamic parameters)
        {
            //ovo ce se naknadno dodati

            /* TODO:
             * check user cookie
             * check if post exists
             * check if post visible to user
             * add the comment to the database
             * return status code
             */


            return null;
        }

        //method used to handle the action of creating a new post
        public dynamic Create(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * check sender ID - za ova dva polje ako ces inheritat ono, stavi u klasu neka inherita i da li posotji user funkciju(userExists iz FriendsFunctionGroup)
             * check receiver ID
            */
            //check where is post(on users profile or on another wall)
            try
            {
                if(isPostVisible(parameters.creatorId, parameters.targetId))
                {
                    createPost(parameters.creatorId, parameters.targetId, parameters.postContent);
                }
                else
                {
                    //neka baci exception da nisu prijatelji
                }
            }
            catch (Exception ex)
            {
                throw;
            } 

             
             /* return status code
             */
            return null;
        }

        //method used to handle the action of loading a post
        public dynamic Load(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * check if the user has the privileges to see the post
             * extract the post from the database
             * return a model
             */
            return null;
        }

        /// <summary>
        /// @postExists checks if post exists
        /// </summary>
        /// <param name="postId">int. User's Id </param>
        /// <returns>
        /// Returns true if post exists, otherwise returns false</returns>
        private bool postExists(int postId)
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
        private bool isPostVisible(int creatorId, int targetId)
        {
            //ovdje bi mu trebao baciti inheritance. treba ubaciti u Function group metodu iz FriendsFunctionGroup-a getAllFriendsId. Ona treba vratiti listu prijatelja jednog usera.
            List<int> friends = getAllFriendsId(targetId);

            return friends.Contains(creatorId);
        }

        /// <summary>
        /// @isLiked checks if user liked certain post
        /// </summary>
        /// <param name="userId">int. User's Id</param>
        /// <param name="postId">int. Post Id</param>
        /// <returns>
        /// Returns true if user liked, else returns false</returns>

        private bool isLiked(int userId, int postId)
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
        private void addLike(int userId, int postId)
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
}
