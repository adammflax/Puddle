using System;

namespace Puddle.parsing.Objects
{
    public class Links
    {
        public Links(string rel, String href)
        {
            Rel = rel;
            Href = href;
        }

        public string Rel { get; private set; }
        public string Href { get; private set; }

        public override string ToString()
        {
            return string.Format("\n<link rel='{0}' href='{1}'/>", Href, Rel);
        }

    }
}
