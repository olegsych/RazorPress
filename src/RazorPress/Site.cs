using System.Collections.Generic;

namespace RazorPress
{
    /// <summary>
    /// Represents a web site being generated.
    /// </summary>
    public class Site
    {
        private readonly IList<Page> pages = new List<Page>();
        private readonly IList<Tag> tags = new List<Tag>();

        /// <summary>
        /// Gets a list of <see cref="Page"/>s.
        /// </summary>
        public IList<Page> Pages 
        {
            get { return this.pages; }
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
