using System.Composition;
using System.Diagnostics.CodeAnalysis;

namespace RazorPress.Build
{
    /// <summary>
    /// Updates <see cref="Page"/> properties from its header section.
    /// </summary>
    [Export]
    public class ReadRazorPageHeaders : RazorPageCommand
    {
        /// <summary>
        /// Executes the header section of a Razor page to read page properties before further processing.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This method is called only by the base class.")]
        protected override void Execute(Page page)
        {
            // TODO: Find a way to read razor header without rendering the entire page.
            this.Transform(page, page.Content);
        }
    }
}
