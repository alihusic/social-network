using Nancy;
using Nancy.ModelBinding;
using SocialNetwork;
using SocialNetwork.Model;
using System;

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
            Get["/populate"] = _ =>
            {
                try
                {
                    var context = new SocialNetworkDBContext();

                    var user = new User()
                    {
                        name = "ermin",
                        lastName = "sabotic",
                        username = "ermin1",
                        password = "dsada",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        region = "balkan"
                    };

                    context.users.Add(user);
                    context.SaveChanges();

                    var user1 = new User()
                    {
                        name = "Nedim",
                        lastName = "Sladic",
                        username = "nedim1",
                        password = "dsada",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        region = "balkan"
                    };

                    context.users.Add(user1);
                    context.SaveChanges();

                    var user2 = new User()
                    {
                        name = "Ali",
                        lastName = "Husic",
                        username = "ali1",
                        password = "dsada",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        region = "balkan"
                    };
                    context.users.Add(user2);
                    context.SaveChanges();

                    var user3 = new User()
                    {
                        name = "Tarik",
                        lastName = "Pasic",
                        username = "tarik1",
                        password = "dsada",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        region = "balkan"
                    };

                    context.users.Add(user3);
                    context.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw;
                }
                return "OK";
            };

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