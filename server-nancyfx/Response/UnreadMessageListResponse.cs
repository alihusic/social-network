using SocialNetwork2.Model;
using System.Collections.Generic;

namespace SocialNetwork2.Response
{
    public class UnreadMessageListResponse:StatusResponse
    {
        public List<UnreadMessage> unreadMessageList { get; set; }
        public UnreadMessageListResponse(List<UnreadMessage> unreadMessageList) : base(true)
        {
            this.unreadMessageList = unreadMessageList;
        }
    }
}