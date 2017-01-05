using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork2.Model
{


    /// <summary>
    /// Class used as database model for PrivateMessage table.
    /// Class created by Ermin.
    /// </summary>

    public class PrivateMessage
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
