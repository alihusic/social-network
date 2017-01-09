using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Response
{
    public class ErrorResponse:StatusResponse
    {
        public string errorMessage { get; set; }
        public ErrorResponse(string errorMessage):base(false)
        {
            this.errorMessage = errorMessage;
        }
    }
}