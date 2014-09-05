using System.ComponentModel.Composition;
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
        protected override void Execute(Page page)
        {
            page.Content = this.markdown.Transform(page.Content);
        }
    }
}
