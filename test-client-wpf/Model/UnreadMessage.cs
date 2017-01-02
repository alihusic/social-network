using System;

namespace SocialNetwork2.Model
{

    //model for UnreadMessages table
    //used for storing yet unread messages

    public class UnreadMessage
    {
        public int unreadMessageId { get; set; }
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
