using SocialNetwork2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Response
{
    public class TokenResponse:StatusResponse
    {
        public Token userToken { get; set; }
        public TokenResponse(Token userToken) : base(true)
        {
            this.userToken = userToken;
        }
    }
}