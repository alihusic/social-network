using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testClientWPF
{
    public class SNRequestBuilder
    {
        private string requestBody { get; set; }
        private string urlSubPath { get; set; }
        private string requestMethod { get; set; }
        private string accept { get; set; }
        private string contentType { get; set; }

        public SNRequestBuilder RequestBody(string requestBody)
        {
            this.requestBody = requestBody;
            return this;
        }

        public SNRequestBuilder UrlSubPath(string urlSubPath)
        {
            this.urlSubPath = urlSubPath;
            return this;
        }

        public SNRequestBuilder RequestMethod(string requestMethod)
        {
            this.requestMethod = requestMethod;
            return this;
        }

        public SNRequestBuilder Accept(string accept)
        {
            this.accept = accept;
            return this;
        }

        public SNRequestBuilder ContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public SNRequest Build()
        {
            return new SNRequest
            {
                urlSubPath = this.urlSubPath,
                requestBody = this.requestBody,
                requestMethod = this.requestMethod,
                accept = this.accept,
                contentType = this.accept
            };
        }
    }
}
