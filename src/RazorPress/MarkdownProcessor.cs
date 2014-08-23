using MarkdownDeep;

namespace RazorPress
{
    internal class MarkdownProcessor
    {
        private readonly Markdown markdown = new Markdown();

        internal string Render(string template)
        {
            return this.markdown.Transform(template);
        }
    }
}
