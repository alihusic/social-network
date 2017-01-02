using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace testClientWPF
{
    public class SNServiceRequest
    {
        public string requestBody { get; set; }
        public string urlSubPath { get; set; }
        public string requestMethod { get; set; }
        public string accept { get; set; }
        public string contentType { get; set; }
       
        public string requestFromServer()
        {
            string urlPath = ServerData.getServerURL()+urlSubPath;

            var request = (HttpWebRequest)WebRequest.Create(urlPath);

            request.Accept = accept;
            request.ContentType = contentType;

            var data = Encoding.ASCII.GetBytes(requestBody);

            request.Method = requestMethod;
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();

            return responseString;
        }
    }
}
