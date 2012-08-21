using IsolatedStorage.Properties;
using IsolatedStorage.oAuth2;
using Machine.Specifications;

namespace IsolatedStorage.Tests
{
    public class When_using_a_good_auth_code
    {
        private static GetAuthCode authcode;
        static AuthorizationCodeTokenRequest request;
        static TokenClient client;
        static TokenResponse Result;

         Establish context = () =>
             // fetch an auth code
                                 {
                                     authcode = new GetAuthCode();

                                     request = new AuthorizationCodeTokenRequest(Settings.Default.ClientId, "5522f8ca-8c59-414f-9ffd-49313aa70a7c", Settings.Default.RedirectUri);
                                     client = new TokenClient();
                                 };

        private Because we_fetch_the_token = () => Result = client.GetToken(request);

        private It should_have_content = () => Result.ShouldNotBeNull();


    }
}