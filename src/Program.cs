using System.Configuration;
using System.IO.IsolatedStorage;
using System.Text;
using IsolatedStorage.Properties;
using IsolatedStorage.oAuth2;
using ServiceStack.Text;


namespace IsolatedStorage
{
    public class Token
    {
        private const string Path = "@accessToken.txt";
        private static readonly IsolatedStorageFile IsolatedFile = IsolatedStorageFile.GetMachineStoreForAssembly();

        public static void Main()
        {
            /*
            RootObject token;

            //check if a token can be retrieved from the file
            if (IsolatedFile.FileExists(Path))
            {

                var readToken = new ReadAccessTokenFromFile();
                byte[] encryptedToken = readToken.ReadFile(Path, IsolatedFile);
                var decrypt = new DecryptData(encryptedToken);
                token = JsonSerializer.DeserializeFromString<RootObject>(Encoding.UTF8.GetString(decrypt.DecryptedData)); //when we decrypt it the response will be a byte[] we need it as a string 

                System.Console.WriteLine("Retrieved token from file. Your token is: " + token.access_token);
            }
            else //other wise we need to get a auth code --> get a access token --> save it to the file
            {
                var accessToken = StartBrowserAndGetAuthCode();
                System.Console.WriteLine("Please Enter the Auth code into the console!");
                token = accessToken.GetAccessTokenFromHuddle(System.Console.ReadLine());  //readLine == auth_code
                accessToken.SaveAccessToken(token, Path, IsolatedStorageFile.GetMachineStoreForAssembly());

                System.Console.WriteLine("Your access Token is: "+ token.access_token);
            }
            System.Console.ReadLine(); //cheat way to pause the program*/
        }
    }
}