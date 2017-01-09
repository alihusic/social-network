using SocialNetwork2.Model;
using System.Collections.Generic;

namespace SocialNetwork2.Response
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