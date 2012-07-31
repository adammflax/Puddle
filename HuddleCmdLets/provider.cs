using System;
using System.Management.Automation;
using System.Management.Automation.Provider;
using DynamicRest.Fluent;
using IsolatedStorage.Provider;

namespace HuddleCmdLets
{
    [CmdletProvider("Puddle", ProviderCapabilities.None)]
    class Provider : ContainerCmdletProvider
	{
        private const string AcceptHeader = "application/vnd.huddle.data+xml";
        private const String HardCodedFolderUri = "http://api.huddle.dev/files/folders/1239695";
        private readonly ProviderParameters _parameters = new ProviderParameters();

        private Folder GetFolderFromPath()
        {

            var restClientBuilder = new RestClientBuilder()
                .WithUri(HardCodedFolderUri)
                .WithOAuth2Token(_parameters.Token)
                .WithAcceptHeader(AcceptHeader);

            var response = restClientBuilder.Build().Get();

            return new BuildFolder(response);
        }

        private Document GetDocumentFromPath()
        {

            var restClientBuilder = new RestClientBuilder()
                .WithUri(HardCodedFolderUri)
                .WithOAuth2Token(_parameters.Token)
                .WithAcceptHeader(AcceptHeader);

            var response = restClientBuilder.Build().Get();

            return new BuildDocument(response);
        }


        protected override void GetItem(string type)
        {
            if(type == "folder")
            {
                var folder = GetFolderFromPath();
                if (folder != null)
                {
                    WriteItemObject(folder, HardCodedFolderUri, false);
                }
            }
            else if (type == "document")
            {
                var document = GetDocumentFromPath();
                if (document != null)
                {
                    WriteItemObject(document, HardCodedFolderUri, false);
                }
            }
        }

        protected override bool IsValidPath(string path)
        {
            throw new NotImplementedException();
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive is HuddleProviderInfo)
            {
                return drive;
            }

            var libraryParams = this.DynamicParameters as ProviderParameters;
            return new HuddleProviderInfo(driveInfo: drive, driveParams: libraryParams);
        }

        protected override object NewDriveDynamicParameters()
        {
            return new ProviderParameters();
        }

	}
}
