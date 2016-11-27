using Nancy;
using Nancy.ModelBinding;
using SocialNetwork.Model;

namespace SocialNetworkServerNV1
{
    public class RootRoutes : NancyModule
    {
        //handlers - a group of handler functions enforcing encapsulation to improve reusability and readability


        //private HandlerFunctionGroup handlers = new HandlerFunctionGroup();
        
        //method used to force the module to pack the received object into a model for response 
        public dynamic returnNegotiate(object obj)
        {
            return Negotiate.WithModel(obj);
        }

        /** private void mapRoutes() -> called by the constructor to map the request routes to dynamic functions used to handle them to make code more readable
         */
        private void mapRoutes()
        {
            
            //easter egg
            Get["/"] = _ => "hail, crawler";


            //Newsfeed handling routes
            

            //User handling routes
            

           

            //Post handling routes
            

            //Settings handling routes
            
        }
        public RootRoutes()
        {
            
            mapRoutes();
        }
    }
}