using System;
using System.Net;

namespace SocialNetwork2.Request
{
    /// <summary>
    /// Class used as base class for all requests.
    /// Class created by Ermin & Ali.
    /// </summary>
    public abstract class SNRequest
    {
        public DateTime timeStamp;
        public string ipAddress;
        public SNRequest()
        {
            timeStamp = DateTime.Now;
            ipAddress = new WebClient().DownloadString("http://icanhazip.com");
        }
    }
}