using System;
using System.Text;
using DynamicRest;
using DynamicRest.Fluent;
using DynamicRest.HTTPInterfaces;
using IsolatedStorage;
using IsolatedStorage.Properties;
using IsolatedStorage.oAuth2;
using Puddle.ProviderInfo;
using System.Net;

namespace Puddle.Requests.HttpRequests
{
    class HttpRequests
    {
        private const string AcceptHeader = "application/vnd.huddle.data+xml";
        private static readonly TokenClient Client = new TokenClient();
        private readonly HuddleDocumentLibraryInfo _drive;

        public HttpRequests(HuddleDocumentLibraryInfo drive)
        {
            this._drive = drive;
        }

        public TokenResponse RefreshToken()
        {
            var request = new RefreshTokenRequest(Settings.Default.ClientId, _drive.GetRefreshToken());
            return Client.GetToken(request);
        }

        public RestOperation GetHttpRequestFolderOrDocument(string uri, HuddleDocumentLibraryInfo drive)
        {
            var restClientBuilder = new RestClientBuilder()
                .WithUri(uri)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithAcceptHeader(AcceptHeader);
            var response = restClientBuilder.Build().Get();
            return response;
        }


        public RestOperation GetHttpDeleteRequestFolderOrDocument(string uri, HuddleDocumentLibraryInfo drive)
        {
            var restClientBuilder = new RestClientBuilder()
                .WithUri(uri)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithAcceptHeader(AcceptHeader);
            var response = restClientBuilder.Build().Delete();
            return response;
        }

        public RestOperation GetHttpPostRequestFolderOrDocument(string uri, HuddleDocumentLibraryInfo drive, string body)
        {
            var restClientBuilder = new RestClientBuilder()
                .WithUri(uri)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithBody(body)
                .WithAcceptHeader(AcceptHeader);
            var response = restClientBuilder.Build().Post();
            return response;
        }

        public RestOperation GetHttpPutRequestFolderOrDocument(string uri, HuddleDocumentLibraryInfo drive, string body)
        {
            var restClientBuilder = new RestClientBuilder()
                .WithUri(uri)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithBody(body)
                .WithAcceptHeader(AcceptHeader);
            var response = restClientBuilder.Build().Put();
            return response;
        }

        public RestOperation GetUploadDocumentContent(string uri, HuddleDocumentLibraryInfo drive, string filePath)
        {
            string boundary = "uploadDocumentContentBoundaryString " + Guid.NewGuid();
            var restClientBuilder = new RestClientBuilder()
                .WithUri(uri)
                .WithContentType("multipart/form-data; boundary=" + boundary)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithBody(GetMutiPartData(filePath, boundary))
                .WithAcceptHeader(AcceptHeader);
            var response = restClientBuilder.Build().Put();
            return response;
        }

        private string GetMutiPartData(String filePath, String boundary)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("--[" + boundary + "]--");

            return builder.ToString();
        }

        public RestOperation GetHttpEntryPoint(string uri, HuddleDocumentLibraryInfo drive)
        {
            var firstResponse = new RestClientBuilder()
                .WithUri(uri)
                .WithOAuth2Token(drive.GetAccessToken())
                .WithAcceptHeader(AcceptHeader)

                .Build().Get();

            var secondResponse = new RestClientBuilder()
                .WithUri(firstResponse.GetResponseHeader(HttpResponseHeader.Location))
                .WithOAuth2Token(drive.GetAccessToken())
                .WithAcceptHeader(AcceptHeader)
                .Build().Get();

            return secondResponse;
        }
    }
}
