using Xunit;

namespace RazorPress
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
            string output = processor.Render("# Header");
            Assert.Equal("<h1>Header</h1>", output.TrimEnd());
        }
    }
}
