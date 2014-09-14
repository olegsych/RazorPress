using System.Composition;
using System.Diagnostics.CodeAnalysis;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms Razor template.
    /// </summary>
    [Export]
    public class TransformRazorPages : RazorPageCommand
    {
        /// <summary>
        /// Transforms <see cref="Page.Content"/> using Razor template engine.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This method is called only by the base class.")]
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
