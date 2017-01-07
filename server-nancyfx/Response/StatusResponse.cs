using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetworkServerNV1.Response
{
    public class StatusResponse
    {
        public bool isSuccessful { get; set; }
        public StatusResponse(bool isSuccessful)
        {
            this.isSuccessful = isSuccessful;
        }
    }
}