using System;
using System.Collections;
using System.Collections.Generic;
using DynamicRest;

namespace Puddle.parsing.Objects
{
    internal class SubFolderFactory
    {
        private readonly RestOperation _response;

        public SubFolderFactory(RestOperation response)
        {
            _response = response;
        }

        public IEnumerable<Folder> CreateSubFolders()
        {
            //todo:refactor --> null, --> stop train wreck code (law of demeter) +  less conditional
            if(_response.Result.folders.Count == 0)
            {
                yield break;
            }

           if(_response.Result.folders.Count == 1)
           {
               var fd = _response.Result.folders.folder;
               yield return new Folder(fd.title,fd.description, fd.created, fd.updated, BuildLinks(fd));
               yield break;
           }

            foreach (dynamic fd in _response.Result.folders.folder)
            {
                yield return new Folder(fd.title, fd.description, fd.created, fd.updated, BuildLinks(fd));
            }
        }

        private IEnumerable<Links> BuildLinks(dynamic folder)
        {
            foreach (dynamic li in folder.link)
            {
                yield return new Links(li.rel, li.href);
            }
        }
    }
}                                 