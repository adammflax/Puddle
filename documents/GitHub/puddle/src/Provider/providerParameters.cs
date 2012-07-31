using System.Management.Automation;

namespace IsolatedStorage.Provider
{
    public class ProviderParameters
    {
        [Parameter(Mandatory = false)]
        public string Token { get; set; }
    }
}
