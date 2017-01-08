using System.Collections.Generic;
using TestClientSN.Model;

namespace SocialNetworkServerNV1.Response
{
    public class UserFriendsInfoListResponse:StatusResponse
    {
        public List<UserFriendsInfo> userFriendsInfoList { get; set; }
        public UserFriendsInfoListResponse(List<UserFriendsInfo> userFriendsInfoList) : base(true)
        {
            this.userFriendsInfoList = userFriendsInfoList;
        }
    }
}