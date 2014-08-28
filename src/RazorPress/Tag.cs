using System;
using System.Collections.Generic;

namespace RazorPress
{
    /// <summary>
    /// Represents a tag applied to a set of <see cref="Pages"/>.
    /// </summary>
    public class Tag
    {
        private readonly string name;
        private readonly IList<Page> pages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        public Tag(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Tag name cannot be empty.");
            }

            this.name = name;
            this.pages = new List<Page>();
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        public string Name 
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets a list of <see cref="Page"/> to which this tag is applied.
        /// </summary>
        public IList<Page> Pages 
        {
            get { return this.pages; }
        }
    }
}
