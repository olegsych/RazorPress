using System;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Build
{
    /// <summary>
    /// Updates <see cref="Page"/> properties from its header section.
    /// </summary>
    public class ExecuteRazorPageHeaders : RazorPageCommand
    {
        /// <summary>
        /// Executes the header section of a Razor page to read page properties before further processing.
        /// </summary>
        protected override void Execute(Page page)
        {
            // TODO: Find a way to read razor header without rendering the entire page.
            this.Transform(page, page.Content);
        }
    }
}
