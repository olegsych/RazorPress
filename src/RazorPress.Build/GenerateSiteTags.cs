using System;
using System.Linq;

namespace RazorPress.Build
{
    /// <summary>
    /// Creates <see cref="Site.Tags"/> for all tagged <see cref="Site.Pages"/>.
    /// </summary>
    public class GenerateSiteTags : Command
    {
        /// <summary>
        /// Adds each <see cref="Page"/> to a new or existing <see cref="Tag"/> of the web <see cref="Site"/>.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            foreach (Page page in this.Site.Pages)
            {
                foreach (string tagName in page.Tags)
                {
                    Tag tag = this.Site.Tags.SingleOrDefault(t => string.Equals(t.Name, tagName, StringComparison.CurrentCultureIgnoreCase));
                    if (tag == null)
                    {
                        tag = new Tag(tagName);
                        this.Site.Tags.Add(tag);
                    }

                    tag.Pages.Add(page);
                }
            }
        }
    }
}
