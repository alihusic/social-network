using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Model
{

    
     //Creation of private messages model.
     //Will be used for saving messages that are read
   
    public class PrivateMessages
    {
        public int privateMessageId { get; set; }
        public string messageText { get; set; }
        public int senderId { get; set; }
        public int recipientId { get; set; }
        public DateTime messageTimeStamp { get; set; }
        public int chatID { get; set; }

    }
}
