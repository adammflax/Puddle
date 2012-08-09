using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using Puddle.ProviderInfo;
using Puddle.Requests;
using Puddle.parsing.Objects;


namespace Puddle
{
    [CmdletProvider("Puddle", ProviderCapabilities.ShouldProcess | ProviderCapabilities.Filter)]
    public class HuddleDocumentLibraryProvider : NavigationCmdletProvider
    {
        private ProviderRequests _request;



        protected override void GetItem(string path)
        {
            _request = new ProviderRequests(this.PSDriveInfo as HuddleDocumentLibraryInfo);

            if (IsAFolder(path))
            {
                var folder = _request.GetFolderFromPath(path);
                if (folder != null)
                {
                    WriteItemObject(folder, path, true);
                }
            }
            if(IsADocument(path))
            {
                var document = _request.GetDocumentFromPath(path);
                if(document != null)
                {
                    WriteItemObject(document, path, true);
                }
            }
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            _request = new ProviderRequests(this.PSDriveInfo as HuddleDocumentLibraryInfo);
            if (IsRootPath(path))
            {
                var workspace = _request.GetEntryPoint();
                if (workspace != null)
                {
                    foreach (var ws in workspace)
                    {
                        WriteItemObject(ws, ws.Link.FirstOrDefault(l => l.Rel == "self").ToString(), false);
                    }
                }
            }

            if (IsAFolder(path))
            {
                var folder = _request.GetSubFolderFromPath(path);
                if(folder != null)
                {
                    foreach (var fd in folder)
                    {
                        WriteItemObject(fd, fd.Link.FirstOrDefault(l => l.Rel == "self").ToString(), false);
                    }
                }

                var document = _request.GetSubDocumentFromPath(path);
                if (document != null)
                {
                    foreach (var dc in document)
                    {
                        WriteItemObject(dc, dc.Link.FirstOrDefault(l => l.Rel == "self").ToString(), false);
                    }
                }
            }
        }

        protected override string MakePath(string parent, string child)
        {
            return base.MakePath("", child);
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            _request = new ProviderRequests(this.PSDriveInfo as HuddleDocumentLibraryInfo);

            PSObject fileEntity = PSObject.AsPSObject(newItemValue);

            var fileEntityValue = fileEntity.Properties.ToArray();

            if (itemTypeName == "folder")
            {
                String xmlBody = String.Format("<folder title='{0}' description='{1}' />",fileEntityValue[1].Value, fileEntityValue[0].Value);
                _request.CreateFileOrFolder(path, xmlBody);
            }

            if (itemTypeName == "document" || itemTypeName == "file")
            {
                if(!IsADocumentLibary(path))
                {
                    path = path = "/documents";
                }
                String xmlBody = String.Format("<document title='{0}' description='{1}' />", fileEntityValue[1].Value, fileEntityValue[0].Value);
                _request.CreateFileOrFolder(path, xmlBody);
            }
        }


        protected override void SetItem(string path, object value)
        {
            _request = new ProviderRequests(this.PSDriveInfo as HuddleDocumentLibraryInfo);
            PSObject fileEntity = PSObject.AsPSObject(value);
            var fileEntityValue = fileEntity.Properties.ToArray();

            var libraryParams = DynamicParameters as SetItemLibraryParameters;

            if (IsAFolder(path))
            {
                var folder = _request.GetFolderFromPath(path);
                folder.Title = fileEntityValue[1].Value.ToString();
                folder.Description = fileEntityValue[0].Value.ToString();

                _request.EditFileOrFolder(path + "/edit", folder.ToString());
            }

            if(IsADocument(path))
            {
                if (libraryParams.FileUploadType == null || libraryParams.FileUploadType == "edit" || libraryParams.FileUploadType == "both")
                {
                    var document = _request.GetDocumentFromPath(path);
                    document.Title = fileEntityValue[1].Value.ToString();
                    document.Description = fileEntityValue[0].Value.ToString();

                    _request.EditFileOrFolder(path + "/edit", document.ToString());
                }

                if (libraryParams.FileUploadType == "upload" || libraryParams.FileUploadType == "both")
                {
                    
                }

            }
        }

        protected override object SetItemDynamicParameters(string path, object value)
        {
            return new SetItemLibraryParameters();
        }

        protected override string GetChildName(string path)
        {
            const string pattern = @"[\d]+";
            Match match = Regex.Match(path, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return match.Groups[0].Value;
        }

        protected override string GetParentPath(string path, string root)
        {
            const string pattern = @".+[^/]+/";

            Match match = Regex.Match(path, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return root + match.Groups[1].Value;
            }

            return root + match.Groups[0].Value;

        }

        private Boolean IsADocumentLibary(String path)
        {
           var match = Regex.Match(path,"^((?!documents).)*$");

           if (match.Success)
           {
               return false;
           }

           return true;
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            _request = new ProviderRequests(this.PSDriveInfo as HuddleDocumentLibraryInfo);
            _request.DeleteFileOrFolder(path);
        }

        protected override bool IsItemContainer(string path)
        {
            return true;
        }

        protected override bool HasChildItems(string path)
        {
            return true;
        }

        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override bool ItemExists(string path)
        {
            var drive = PSDriveInfo as HuddleDocumentLibraryInfo;
            _request = new ProviderRequests(drive);

            if (IsRootPath(path))
            {
                return _request.GetEntryPoint() != null;
            }

            if (IsAFolder(path))
            {
                return _request.GetFolderFromPath(path) != null;
            }

            return false;
        }

        private static bool IsRootPath(string path)
        {
            path = Regex.Replace(path, @"^.+:[/\\]?", "");
            return String.IsNullOrEmpty(path);
        }

        private static bool IsADocument(string path)
        {
            var match = Regex.Match(path, @"(?:membership::)?(?:\w+:[\\/])?(?<document>.*)$");

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        private static bool IsAFolder(string path)
        {
            var match = Regex.Match(path, @"(?:membership::)?(?:\w+:[\\/])?(?<folder>.*)$");

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive is HuddleDocumentLibraryInfo)
            {
                return drive;
            }

            var libraryParams = DynamicParameters as DocumentLibraryParameters;
            return new HuddleDocumentLibraryInfo(driveInfo: drive, driveParams: libraryParams);
        }

        protected override object NewDriveDynamicParameters()
        {
            return new DocumentLibraryParameters();
        }
    }
}
