using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    //model for privateChat table
    //will be used to store all  chats

    public class PrivateChat
    {
        public int privateChatId { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public DateTime chatCreationDate { get; set; }
    }
}
