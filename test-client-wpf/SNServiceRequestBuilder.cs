    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testClientWPF
{
    public class SNServiceRequestBuilder
    {
        private string requestBody { get; set; }
        private string urlSubPath { get; set; }
        private string requestMethod { get; set; }
        private string accept { get; set; }
        private string contentType { get; set; }

        public SNServiceRequestBuilder RequestBody(string requestBody)
        {
            this.requestBody = requestBody;
            return this;
        }

        public SNServiceRequestBuilder UrlSubPath(string urlSubPath)
        {
            this.urlSubPath = urlSubPath;
            return this;
        }

        public SNServiceRequestBuilder RequestMethod(string requestMethod)
        {
            this.requestMethod = requestMethod;
            return this;
        }

        public SNServiceRequestBuilder Accept(string accept)
        {
            this.accept = accept;
            return this;
        }

        public SNServiceRequestBuilder ContentType(string contentType)
        {
            this.contentType = contentType;
            return this;
        }

        public SNServiceRequest Build()
        {
            return new SNServiceRequest
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
