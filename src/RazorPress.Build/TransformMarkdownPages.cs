using System.Composition;
using System.Diagnostics.CodeAnalysis;
using MarkdownDeep;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms markdown to HTML.
    /// </summary>
    [Export]
    public class TransformMarkdownPages : PageCommand
    {
        private readonly Markdown markdown = new Markdown();

        /// <summary>
        /// Transforms <see cref="Page.Content"/> from markdown to HTML format.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This method is called only by the base class.")]
        protected override void Execute(Page page)
        {
            page.Content = this.markdown.Transform(page.Content);
        }
    }
}
