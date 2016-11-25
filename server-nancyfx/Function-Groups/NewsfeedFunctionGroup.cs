using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using Nancy;

namespace SocialNetworkServerNV1
{
    public class NewsfeedFunctionGroup
    {
        //method used to handle loading the newsfeed
        public dynamic Load(dynamic parameters)
        {
            /*todo:
             * check user cookie
             * get interval of last [a,b] posts
             * check if there exists that many
             * extract from database
             * return model
             */
            return null;
        }
    }

    
}