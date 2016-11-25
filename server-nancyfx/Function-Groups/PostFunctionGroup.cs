using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    public class PostFunctionGroup
    {
        //method used to handle the action of liking a post
        public dynamic Like(dynamic parameters)
        {
            /* TODO:
             * check user cookie
             * check if post exists
             * check if post visible to user
             * check if user already liked -> remove like
             *                       else -> add like
             * return status code
             */ 
            return null;
        }

        //method used to handle the action of commenting on a post
        public dynamic Comment(dynamic parameters)
        {
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
             * check sender ID
             * check receiver ID
             * check if friendship exists or the post is on the users own wall
             * add the post to the database
             * return status code
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
    }
}