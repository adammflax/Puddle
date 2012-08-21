using System;
using System.Collections.Generic;
using System.Xml;
using DynamicRest;
using Puddle.parsing.Objects;
using Puddle.parsing.Objects.Entities;

namespace Puddle
{
    internal class FolderFactory
    {
        private readonly RestOperation _response;

        public FolderFactory(RestOperation response)
        {
            _response = response;
        }

        public static implicit operator Folder(FolderFactory factory)
        {
            return factory.Build();
        }

        private Folder Build()
        {
            Folder folder;
            try
            {
                folder = new Folder(
                    _response.Result.title, _response.Result.description,
                    DateTime.Parse(_response.Result.created),
                    DateTime.Parse(_response.Result.updated),
                    BuildLinks(_response.Result.link));
            }
            catch (XmlException ex) 
            {
                try //sometimes its just that the folder doesn't have a description so lets try that else there can be no redemption
                {
                    folder = new Folder(
                                   _response.Result.title, "",
                                   DateTime.Parse(_response.Result.created),
                                   DateTime.Parse(_response.Result.updated),
                                   BuildLinks(_response.Result.link));     
                }
                catch (XmlException e)
                {
                    return null;
                }
           
            }

            return folder;
        }

        private IEnumerable<Links> BuildLinks(dynamic result)
        {
            foreach (dynamic li in result)
            {
                yield return new Links(li.rel, li.href);
            }
        }

    }
}