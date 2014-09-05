using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class TransformRazorPagesTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(TransformRazorPages).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(TransformRazorPages)));
        }

        [Fact]
        public void ClassIsDiscoverableDuringComposition()
        {
            var catalog = new AssemblyCatalog(typeof(Command).Assembly);
            var container = new CompositionContainer(catalog);
            var command = container.GetExportedValue<TransformRazorPages>();
            Assert.NotNull(command);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(TransformRazorPages).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsInheritedMethodForConsistentErrorHandling()
        {
            var command = new TransformRazorPages();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteTransformsContentOfGivenPage()
        {
            var page = new Page("/index.html")
            {
                Title = "Hello, World",
                Content = "@Model.Page.Title",
            };
            var command = new TransformRazorPages();
            command.Site = new Site { Pages = { page } };

            command.Execute();
            
            Assert.Equal(page.Title, page.Content);
        }

        [Fact]
        public void ExecuteDoesNothingWithAnEmptyContentBecauseUserMayCreateAnEmptyFile()
        {
            var page = new Page("/index.html");
            var command = new TransformRazorPages();
            command.Site = new Site { Pages = { page } };

            command.Execute();

            Assert.Equal(string.Empty, page.Content);
        }       
    }
}
