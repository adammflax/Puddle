using System;
using System.Collections.Generic;
using System.Text;

namespace Puddle.parsing.Objects.Entities
{
    public class Document
    {
        private readonly IEnumerable<Links> _links;
        private readonly IEnumerable<Actor> _actors;
        public Document(string title, DateTime createdDt, DateTime updatedDt, IEnumerable<Links> links, string description, IEnumerable<Actor> actors, int size, string version)
        {
            Description = description;
            Title = title;
            Created = createdDt;
            Updated = updatedDt;
            Size = size;
            Version = version;
            this._links = links;
            this._actors = actors;
        }

        public string Description { get; set; }
        public string Title { get; set; }
        public string Version { get; private set; }
        public int Size { get; private set; }
        public IEnumerable<Actor> Actors { get { return _actors; } }
        public IEnumerable<Links> Link { get { return _links; } }
        public DateTime Created { get; private set; }
        public DateTime Updated { get; private set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("<document title='{0}'  description='{1}'>", Title, Description));

            foreach(var li in Link)
            {
                builder.Append(String.Format("<link rel='{0}' href='{1}' />",li.Rel, li.Href));
            }

            foreach(Actor actor in Actors)
            {
                builder.Append(actor.ToString());
            }

            builder.Append("<version>" + Version + "</version>");
            builder.Append("<size>" + Size + "</size>");
            builder.Append("<updated>" + Updated + "</updated>");
            builder.Append("<created>" + Created + "</created>");

            builder.Append("</document>");

            return builder.ToString();
        }
    }
}