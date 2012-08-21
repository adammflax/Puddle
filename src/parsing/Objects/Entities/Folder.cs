using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Puddle.parsing.Objects
{
    public class Folder
    {
        private readonly IEnumerable<Links> _links;
        public Folder(string title, string description, DateTime? createdDt = null, DateTime? updatedDt = null, IEnumerable<Links> links = null)
        {
            Description = description;
            Title = title;
            Created = createdDt;
            Updated = updatedDt;
            this._links = links;
        }

        public string Description { get; set; }
        public string Title { get; set; }
        public IEnumerable<Links> Link { get { return _links; } }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get;set; }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("<folder title='{0}'  description='{1}'>", Title, Description));

            foreach(var li in Link)
            {
                builder.Append(String.Format("<link rel='{0}' href='{1}' />",li.Rel, li.Href));
            }

            builder.Append("</folder>");

            return builder.ToString();
        }
    }
}