using System.IO;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms Razor template.
    /// </summary>
    public class TransformRazorPages : RazorPageCommand
    {
        /// <summary>
        /// Transforms <see cref="Page.Content"/> using Razor templating engine.
        /// </summary>
        protected override void Execute(Page page)
        {
            string template = page.Content;
            if (!string.IsNullOrEmpty(template))
            {
                page.Content = this.Transform(page, template);
            }
        }
    }
}
