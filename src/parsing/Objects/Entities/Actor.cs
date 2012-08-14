using System;
using System.Collections.Generic;
using System.Text;

namespace Puddle.parsing.Objects.Entities
{
    public class Actor
    {
        private readonly IEnumerable<Links> _links;

        public Actor(string name, string email,string rel, IEnumerable<Links> links)
        {
            Name = name;
            Email = email;
            Rel = rel;
            this._links = links;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Rel { get; private set; }
        public IEnumerable<Links> Link { get { return _links; } }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("<actor name='{0}' email='{1}' rel='{2}>", Name,Email, Rel));

            foreach (Links li in Link)
            {
                builder.Append(li.ToString());
            }
            builder.Append("</actor");

            return builder.ToString();
        }
    }
}