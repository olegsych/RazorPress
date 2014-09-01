using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class TransformMarkdownPagesTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(TransformMarkdownPages).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromPageCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(PageCommand).IsAssignableFrom(typeof(TransformMarkdownPages)));
        }

        [Fact]
        public void ExecuteConvertsPageContentFromMarkdownToHtml()
        {
            var page = new Page("/index.html");
            page.Content = "# Header";

            var processor = new TransformMarkdownPages();
            processor.Site = new Site { Pages = { page } };
            processor.Execute();

            Assert.Equal("<h1>Header</h1>", page.Content.TrimEnd());
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(TransformMarkdownPages).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var command = new TransformMarkdownPages();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
