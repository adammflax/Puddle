

using System.Management.Automation;

namespace IsolatedStorage.Provider
{
    public class HuddleProviderInfo : PSDriveInfo
    {
        private ProviderParameters _parameters;
        public HuddleProviderInfo(PSDriveInfo driveInfo, ProviderParameters driveParams) : base(driveInfo)
        {
            _parameters = driveParams;
        }

        public string Token
        {
            get { return _parameters.Token; }
        }
    }
}
