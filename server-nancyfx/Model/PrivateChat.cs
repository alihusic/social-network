using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{

    /// <summary>
    /// Class used as database model for PrivateChat table.
    /// Class created by Ermin.
    /// </summary>

    public class PrivateChat
    {
        [Key]
        public int privateChatId { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public DateTime chatCreationTimeStamp { get; set; }



        //setting up 1:n relation with PrivateMessages
        [JsonIgnore]
        public virtual ICollection<PrivateMessage> privateMessages { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }
    }
}
