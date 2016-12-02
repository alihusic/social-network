using Nancy;
using SocialNetwork;
using SocialNetwork.Model;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Method used to handle newsfeed load request
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List of posts</returns>
        
        public dynamic Load(dynamic parameters)
        {
            //todo:
            // check user cookie
            //get interval of last [a,b] posts
            //check if there exists that many

            //nebitno koliki mu se interval postavi, on ce vratiti onoliko clanova koliko ima u bazi. Kada se vrati lista
            //na klijent moze se provjeriti njena velicina i ako je manja od 10(ili od nekog dogovorenog intervala) neka 
            //to bude signal da nema vise postova
            getRecentPosts(parameters.interval, parameters.userId);

            //extract from database
            //return model

            return null;
        }

        /// <summary>
        /// getRecentPosts is used to retrieve recent posts that user will see on newsfeed
        /// </summary>
        /// <param name="interval">int. Interval of posts</param>
        /// <param name="userId">int. User's Id</param>
        /// <returns></returns>

        private List<Posts> getRecentPosts(int interval, int userId)
        {
            using (var context = new SocialNetworkDBContext())
            {
                var friends = helpers.getAllFriends(userId);
                /* Treba:
                 * note: ovo sam ja bezveze blebetao, zbog sebe samo. neka ga zasad ako mi zatreba ko podsjetnik (Ermin)
                 * ako je userId ili creatorId ili targetId;  
                 * loadat 10 zadnjih postova. 
                 * cuvati id zadnjeg posta da bi se znalo odakle ce se krenuti sljedeci put.
                 * moze se napraviti sa client side-a da se se cuva Id zadnjeg posta koji je ucitan. 
                 * neka se po defaulut prvi put sa klijent strane posalje 0.
                 * ja provjerim ovdje ako je vrijednost 0, neka to bude vrijednost zadnjeg posta. 
                 * svaki sljedeci put ce se vracati na client i updateovat.
                 * takodjer neka se sa klijenta salje i interval. neka je default 10 i neka se incrementa svaki put kada se salje zahtjev.
                */
                return context.posts.Where(p => friends.Contains(p.targetId)).OrderByDescending(p => p.postCreationDate).Skip(interval).Take(interval - (interval - 10)).ToList();
            }
        }
    }
}