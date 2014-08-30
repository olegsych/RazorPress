using MarkdownDeep;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms markdown to HTML.
    /// </summary>
    public class TransformMarkdownPage : PageCommand
    {
        private readonly Markdown markdown = new Markdown();

        /// <summary>
        /// Transforms <see cref="Page.Content"/> from markdown to HTML format.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            this.Page.Content = this.markdown.Transform(this.Page.Content);
        }
    }
}
