using System.Management.Automation;

namespace Puddle.ProviderInfo
{
    public class DocumentLibraryParameters
    {
        [Parameter(Mandatory = true)]
        public string Host { get;set; }

        [Parameter(Mandatory = true)]
        public string Token { get; set; }

    }
}
