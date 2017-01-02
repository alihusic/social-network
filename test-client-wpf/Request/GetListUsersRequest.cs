using System.Collections.Generic;

namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used to create request that handles loading of user id's.
    /// Note: user token not required
    /// </summary>
    public class GetListUsersRequest : UnrestrictedRequest
    {
        public List<int> listUserId { get; set; }
    }
}