using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for privateChat table
    //will be used to store all  chats

    public class PrivateChat
    {
        [Key]
        public int privateChatId { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public DateTime chatCreationDate { get; set; }



        //setting up 1:n relation with PrivateMessages
        public virtual ICollection<PrivateMessages> privateMessages { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }
    }
}
