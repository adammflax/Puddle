using System.Text.RegularExpressions;
using Puddle.ProviderInfo;

namespace Puddle.parsing.LinkManagers
{
    class ManageLinks
    {
        public string ExtractResourceFromPath(string path)
        {
            string resource = string.Empty;
            var match = Regex.Match(path, @"(?:membership::)?(?:\w+:[\\/])?(?<folder>.*)$");

            if (match.Success)
            {
                resource = match.Groups["folder"].Value;
            }
            return resource;
        }

        public string GetFolderUri(string path, HuddleDocumentLibraryInfo drive)
        {
            var apiHost = drive.Host;
            return "https://" + apiHost + "/" + path;
        }

    }
}
