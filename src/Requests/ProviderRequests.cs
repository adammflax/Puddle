using System;
using System.Linq;
using System.Net;
using DynamicRest;
using Puddle.ProviderInfo;
using Puddle.parsing.LinkManagers;
using Puddle.parsing.Objects;
using System.Collections;
using System.Collections.Generic;
using Puddle.parsing.Objects.Entities;

namespace Puddle.Requests
{
    class ProviderRequests
    {
        private readonly ManageLinks _linkManager;
        private readonly HuddleDocumentLibraryInfo _drive;
        private HttpRequests.HttpRequests _requests;

        public ProviderRequests(HuddleDocumentLibraryInfo drive)
        {
            this._drive = drive;
            _linkManager = new ManageLinks();
            _requests = new HttpRequests.HttpRequests(_drive);
        }

        public Folder GetFolderFromPath(string path)
        {
            var response = SendRequest(_requests.GetHttpRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive));

            return new FolderFactory(response);
        }

        public void DeleteFileOrFolder(string path)
        {
            var response = SendRequest(_requests.GetHttpDeleteRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive));
        }

        public void UploadFile(String path, String fileName)
        {
            var response =SendRequest(_requests.GetUploadDocumentContent(_linkManager.GetFolderUri(path, _drive), _drive, fileName));
        }

        public void EditFileOrFolder(string path, string body)
        {
            var response = SendRequest(_requests.GetHttpPutRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive, body));
        }

        public void CreateFolder(string path, string body)
        {
            var response = SendRequest(_requests.GetHttpPostRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive, body));
        }

        public Document CreateFile(string path, string body)
        {
            var response = SendRequest(_requests.GetHttpPostRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive, body));
            return new DocumentFactory(response);
        }

        public Document GetDocumentFromPath(string path)
        {
            var response = SendRequest(_requests.GetHttpRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive));

            return new DocumentFactory(response);
        }

        public IEnumerable<Folder> GetSubFolderFromPath(string path)
        {

            if( path == "" || path ==  "/")
            {
                return null;
            }
            var response = SendRequest(_requests.GetHttpRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive));

            var subFolders = new SubFolderFactory(response);
            return subFolders.CreateSubFolders().ToArray();
        }


        public IEnumerable<Document> GetSubDocumentFromPath(string path)
        {

            if (path == "" || path == "/")
            {
                return null;
            }
            var response = SendRequest(_requests.GetHttpRequestFolderOrDocument(_linkManager.GetFolderUri(path, _drive), _drive));

            var subDocuments = new SubDocumentFactory(response);
            return subDocuments.CreateSubDocument().ToArray();
        }

        public IEnumerable<Workspace> GetEntryPoint()
        {
            string _path = "entry";
            var response = SendRequest(_requests.GetHttpEntryPoint(_linkManager.GetFolderUri(_path, _drive), _drive));
            var entry = new EntryFactory(response);
            return entry.CreateWorkspaces().ToArray();
        }

        private RestOperation SendRequest(RestOperation request)
        {
            var response = request;
            if (response.StatusCode == HttpStatusCode.Unauthorized) //means tokens expired so lets refresh it here
            {
                var token = _requests.RefreshToken();
                _drive.Token = "Access_token: " + token.AccessToken + ",Expires_in: " + token.ExpirySeconds + ", Refresh_token: " + token.RefreshToken;
                response = request;
            }

            return response;
        }
    }
}
