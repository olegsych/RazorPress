using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class ExecuteRazorPageHeadersTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ExecuteRazorPageHeaders).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolimorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(ExecuteRazorPageHeaders)));
        }

        [Fact]
        public void ClassIsDiscoverableDuringComposition()
        {
            var catalog = new AssemblyCatalog(typeof(Command).Assembly);
            var container = new CompositionContainer(catalog);
            var command = container.GetExportedValue<ExecuteRazorPageHeaders>();
            Assert.NotNull(command);
        }

        [Fact]
        public void ExecuteRendersHeaderSectionOfRazorTemplateToInitializePageProperties()
        {
            var page = new Page("/index.html")
            {
                Content = @"
                @{
                    this.Page.Title = ""ExpectedValue"";
                }"
            };
            var command = new ExecuteRazorPageHeaders();
            command.Site = new Site { Pages = { page } };

            command.Execute();

            Assert.Equal("ExpectedValue", page.Title);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClassForPolymorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(ExecuteRazorPageHeaders).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsBaseMethodForConsistentErrorHandling()
        {
            var command = new ExecuteRazorPageHeaders();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
