using SocialNetwork2.Model;
using System.Collections.Generic;

namespace SocialNetworkServerNV1.Response
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