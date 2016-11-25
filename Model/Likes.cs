using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{
    //model for table Likes
    //will be used to store likes info

    public class Likes
    {

        public int likesId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }

        //1:n with users
        public virtual User user { get; set; }

    }
}
