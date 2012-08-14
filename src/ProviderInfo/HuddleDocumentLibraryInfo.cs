using System.Management.Automation;
using System.Text.RegularExpressions;

namespace Puddle.ProviderInfo
{
    public class HuddleDocumentLibraryInfo : PSDriveInfo
    {
        private readonly DocumentLibraryParameters _driveParams;

        public HuddleDocumentLibraryInfo(PSDriveInfo driveInfo, DocumentLibraryParameters driveParams) 
            : base(driveInfo)
        {
            _driveParams = driveParams;
        }

        public string Host
        {
            get { return _driveParams.Host; }
        }


        public string Token
        {
            private get { return _driveParams.Token; }
            set { _driveParams.Token = value; }
        }

        public string GetRefreshToken()
        {
            const string patternStr = ".*access_token\\s*:\\s*\\S+,\\s*expires_in\\s*:\\s*\\d+,\\s*refresh_token\\s*:\\s*(\\S*)";


            Match match = Regex.Match(Token, patternStr, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return match.Groups[0].Value;
        }

        public string GetAccessToken()
        {
            const string patternStr = ".*access_token\\s*:\\s*([^,]+)";
            Match match = Regex.Match(Token, patternStr, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return match.Groups[0].Value;
        }
    }
}
