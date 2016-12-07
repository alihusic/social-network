using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Model
{


    //Creation of private messages model.
    //Will be used for saving messages that are read

    public class PrivateMessages
    {
        [Key]
        public int privateMessageId { get; set; }
        public string messageText { get; set; }
        public int senderId { get; set; }
        public int recipientId { get; set; }
        public DateTime messageTimeStamp { get; set; }
        public int chatID { get; set; }


        //setting up 1:n relation with PrivateChat
        public virtual PrivateChat chat { get; set; }

        //setting up 1:n relation with User
        public virtual User user { get; set; }
    }
}
