using System;
using System.Collections.Generic;
using System.IO;

namespace RazorPress
{
    /// <summary>
    /// Represents a web site being generated.
    /// </summary>
    public class Site
    {
        private readonly IList<Page> pages = new List<Page>();
        private readonly IList<Tag> tags = new List<Tag>();
        private readonly DirectoryInfo source;

        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class with the given directory as <see cref="Source"/>.
        /// </summary>
        public Site(DirectoryInfo source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            this.source = source;
        }

        /// <summary>
        /// Gets a list of <see cref="Page"/>s.
        /// </summary>
        public IList<Page> Pages 
        {
            get { return this.pages; }
        }

        /// <summary>
        /// Gets the source directory of this site.
        /// </summary>
        public DirectoryInfo Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Gets a list of <see cref="Tag"/>s.
        /// </summary>
        public IList<Tag> Tags 
        {
            get { return this.tags; }
        }
    }
}
