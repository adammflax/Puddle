using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IsolatedStorage.HttpRequests
{
    internal class HttpRequests
    {
        /*
        public WebResponse Post(String uri, byte[] postData, Dictionary<string, string> headers = null,
                                String accept = null, String contentType = "")
        {
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = "POST";

            SetRequestHeaders(headers, request);
            SetAccept(accept, request);
            SetContentType(contentType, request);
            request.ContentLength = postData.Length;

            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(postData, 0, postData.Length);
            }

            return request.GetResponse();
        }

        private static void SetRequestHeaders(Dictionary<string, string> headers, HttpWebRequest request)
        {
            if (headers != null)
            {
                foreach (KeyValuePair<String, String> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }
        }

        private static void SetAccept(String accept, HttpWebRequest request)
        {
            if (accept != null)
            {
                request.Accept = accept;
            }
        }

        private static void SetContentType(String contentType, HttpWebRequest request)
        {
            if (contentType != null)
            {
                request.ContentType = contentType;
            }
    }*/   
    }
}

