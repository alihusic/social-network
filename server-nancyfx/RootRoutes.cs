using Nancy;
using SocialNetwork2;
using SocialNetwork2.Model;
using System;

namespace SocialNetwork2
{
    /// <summary>
    /// Class used for starting get request, and for fast population.
    /// Class created by Ermin & Ali.
    /// </summary>
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
                        password = "p",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                        coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png"
                    };

                    context.users.Add(user);
                    context.SaveChanges();

                    var user1 = new User()
                    {
                        name = "Nedim",
                        lastName = "Sladic",
                        username = "nedim1",
                        password = "p",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                        coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png"

                    };

                    context.users.Add(user1);
                    context.SaveChanges();

                    var user2 = new User()
                    {
                        name = "Ali",
                        lastName = "Husic",
                        username = "ali1",
                        password = "p",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                        coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png"

                    };
                    context.users.Add(user2);
                    context.SaveChanges();

                    var user3 = new User()
                    {
                        name = "Tarik",
                        lastName = "Pasic",
                        username = "tarik1",
                        password = "p",
                        city = "Sarajevo",
                        country = "bosna",
                        dateOfBirth = DateTime.Now,
                        gender = "Male",
                        pictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png",
                        coverPictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d3/User_Circle.png"

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