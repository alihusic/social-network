using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1.Response
{
    public class MessageResponse:StatusResponse
    {
        public string responseMessage { get; set; }
        public MessageResponse(string responseMessage) : base(true)
        {
            this.responseMessage = responseMessage;
        }
    }
}