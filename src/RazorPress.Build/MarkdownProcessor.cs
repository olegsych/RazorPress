using MarkdownDeep;

namespace RazorPress.Build
{
    internal class MarkdownProcessor : IPageProcessor
    {
        private readonly Markdown markdown = new Markdown();

        public void Process(Page page)
        {
            page.Content = this.markdown.Transform(page.Content);
        }
    }
}
