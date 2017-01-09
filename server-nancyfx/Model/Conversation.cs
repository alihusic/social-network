using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Model
{
    /// <summary>
    /// Class created by Ermin&Ali
    /// </summary>
    public class Conversation
    {
        public int privateChatId { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }

        public List<PrivateMessage> privateMessages { get; set; }
    }
}