using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1
{
    public class FriendsFunctionGroup
    {
        //method used to add a friend, not very obvious
        public dynamic Add(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * check if user exists - or could handle with exception later
             * check if friendship exists
             * check if pending friendship exists
             * update database
             * return status code
             */
            return null;
        }

        

        //method used to confirm a friend request
        public dynamic Confirm(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * check if pending friendship exists
             * update database
             * return status code
             */
            return null;
        }

        //method used to delete a person from the friend list
        public dynamic Delete(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * check if friendship exists
             * update database
             * return status code
             */
            return null;
        }

        //method used to get a list of all people on the friend list
        public dynamic GetAll(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * simple database query
             * bind the result to a model
             * return model&status code
             */ 
            return null;
        }

        /*todo: 
         * maybe add a check new friendship requests method
         * add a check user cookie method
         * add a check if friendship exists method
         * add a check if pending friendship exists method
         * add a status report class
         */
    }
}