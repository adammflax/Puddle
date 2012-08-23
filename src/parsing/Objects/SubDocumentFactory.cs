using System.Collections.Generic;
using DynamicRest;
using Puddle.parsing.Objects.Entities;

namespace Puddle.parsing.Objects
{
    internal class SubDocumentFactory
    {
        private readonly RestOperation _response;

        public SubDocumentFactory(RestOperation response)
        {
            _response = response;
        }

        public IEnumerable<Document> CreateSubDocument()
        {

            //todo:refactor --> null, --> stop train wreck code (law of demeter) +  less conditional
            var _documentResponse = _response.Result.documents;
            if (_documentResponse.Count == 0)
            {
                yield break;
            }

            if (_documentResponse.Count == 1)
            {
                var dc = _documentResponse.document;
                yield return new Document(dc.title, dc.created, dc.updated, BuildLinks(dc), dc.description, BuildActors(dc),dc.size, dc.version);
                yield break;
            }

            foreach (dynamic dc in _documentResponse.document)
            {
                yield return new Document(dc.title, dc.created, dc.updated, BuildLinks(dc), dc.description, BuildActors(dc),dc.size, dc.version);
            }
        }

        private IEnumerable<Links> BuildLinks(dynamic workspace)
        {
            foreach (dynamic li in workspace.link)
            {
                yield return new Links(li.rel, li.href);
            }
        }

        private IEnumerable<Actor> BuildActors(dynamic workspace)
        {
            foreach (dynamic ac in workspace.actor)
            {
                yield return new Actor(ac.name, ac.email,ac.rel, BuildLinks(ac));
            }
        }
    }
}