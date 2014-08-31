using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class TransformRazorPageTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(TransformRazorPage).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(TransformRazorPage)));
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(TransformRazorPage).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsInheritedMethodForConsistentErrorHandling()
        {
            var command = new TransformRazorPage();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteTransformsContentOfGivenPage()
        {
            var command = new TransformRazorPage();
            command.Site = new Site();
            command.Page = new Page("index.html")
            {
                Title = "Hello, World",
                Content = "@Model.Page.Title",
            };

            command.Execute();
            
            Assert.Equal(command.Page.Title, command.Page.Content);
        }

        [Fact]
        public void ExecuteDoesNothingWithAnEmptyContentBecauseUserMayCreateAnEmptyFile()
        {
            var command = new TransformRazorPage();
            command.Site = new Site();
            command.Page = new Page("index.html");

            command.Execute();

            Assert.Equal(string.Empty, command.Page.Content);
        }       
    }
}
