using System;
using System.Net;
using System.Text;
using DynamicRest;
using DynamicRest.Fluent;
using IsolatedStorage.Properties;

namespace IsolatedStorage
{
    public class TokenClient
    {
        public TokenResponse GetToken(TokenRequest request)
        {
            var client = new RestClientBuilder()
                .WithAcceptHeader("application/json")
                .WithContentType("application/x-www-form-urlencoded")
                .WithBody(request.ToForm())
                .WithUri(Settings.Default.HuddleAuthServer + Settings.Default.HuddleTokenEndPoint)
                .Build().Post();



            if (client.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new OAuthException(client.Result.error, client.Result.error_description, client.Result.error_uri);
            }

            return new TokenResponse
                       {
                           AccessToken = client.Result.access_token,
                           ExpirySeconds = client.Result.expires_in,
                           RefreshToken = client.Result.refresh_token
                       };
        }
    }

    public class OAuthException : Exception
    {
        private readonly string _errorKey;
        private readonly string _errorUri;

        public OAuthException(string errorKey, string message, string errorUri)
            : base(message)
        {
            _errorKey = errorKey;
            _errorUri = errorUri;
        }

        public string ErrorKey
        {
            get { return _errorKey; }
        }

        public string ErrorUri
        {
            get { return _errorUri; }
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpirySeconds { get; set; }

    }
}