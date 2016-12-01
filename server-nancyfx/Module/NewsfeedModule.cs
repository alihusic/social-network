using Nancy;

namespace SocialNetworkServerNV1
{
    public class NewsfeedModule : NancyModule
    {
        private FunctionGroup helpers = new FunctionGroup();

        public NewsfeedModule():base("/newsfeed")
        {
            Get["/"] = _ => "Hello!";
            Get["/load"] = parameters => Load(parameters);
        }

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