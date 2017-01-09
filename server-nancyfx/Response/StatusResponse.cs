using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Response
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