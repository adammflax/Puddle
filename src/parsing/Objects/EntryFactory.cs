using System;
using System.Collections;
using System.Collections.Generic;
using DynamicRest;

namespace Puddle.parsing.Objects
{
    internal class EntryFactory
    {
        private readonly RestOperation _response;

        public EntryFactory(RestOperation response)
        {
            _response = response;
        }

        public IEnumerable<Workspace> CreateWorkspaces ()
        {
            foreach (dynamic ws in _response.Result.membership.workspaces.workspace)
            {
                yield return new Workspace(ws.type, ws.title, BuildLinks(ws));
            }
        }

        private IEnumerable<Links> BuildLinks(dynamic workspace)
        {
            foreach (dynamic li in workspace.link)
            {
                yield return new Links(li.rel, li.href);
            }
        }
    }
}                                 