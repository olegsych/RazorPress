using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class TransformMarkdownPageTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(TransformMarkdownPage).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromPageCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(PageCommand).IsAssignableFrom(typeof(TransformMarkdownPage)));
        }

        [Fact]
        public void ExecuteConvertsPageContentFromMarkdownToHtml()
        {
            var page = new Page("index.html");
            page.Content = "# Header";

            var processor = new TransformMarkdownPage();
            processor.Site = new Site();
            processor.Page = page;
            processor.Execute();

            Assert.Equal("<h1>Header</h1>", page.Content.TrimEnd());
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(TransformMarkdownPage).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var command = new TransformMarkdownPage();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
