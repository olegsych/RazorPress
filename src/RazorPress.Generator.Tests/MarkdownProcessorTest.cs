using System.IO;
using Xunit;

namespace RazorPress.Generator
{
    public class MarkdownProcessorTest
    {
        [Fact]
        public void ClassIsInternalAndNotMeantToBeAccessedByUsers()
        {
            Assert.False(typeof(MarkdownProcessor).IsPublic);
        }

        [Fact]
        public void RenderConvertsMarkdownToHtml()
        {
            var processor = new MarkdownProcessor();
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Content = "# Header";
            processor.Process(page);
            Assert.Equal("<h1>Header</h1>", page.Content.TrimEnd());
        }
    }
}
