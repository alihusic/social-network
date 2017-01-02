using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SocialNetwork2.Model
{

    //model for privateChat table
    //will be used to store all  chats

    public class PrivateChat
    {
        public int privateChatId { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public DateTime chatCreationTimeStamp { get; set; }



        //setting up 1:n relation with PrivateMessage
        [JsonIgnore]
        public virtual ICollection<PrivateMessage> PrivateMessage { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }
    }
}
