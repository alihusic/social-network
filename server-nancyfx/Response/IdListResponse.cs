using System.Collections.Generic;

namespace SocialNetwork2.Response
{
    public class IdListResponse:StatusResponse
    {
        public List<int> idList { get; set; }
        public IdListResponse(List<int> idList) : base(true)
        {
            this.idList = idList;
        }
    }
}