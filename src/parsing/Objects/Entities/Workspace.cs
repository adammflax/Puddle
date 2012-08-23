using System;
using System.Collections;
using System.Collections.Generic;

namespace Puddle.parsing.Objects
{
    public class Workspace
    {
        private readonly IEnumerable<Links> _links;
        public Workspace(string type, string title, IEnumerable<Links> links)
        {
            Type = type;
            Title = title;
            this._links = links;
        }

        public string Type { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<Links> Link { get { return _links; } }
    }
}