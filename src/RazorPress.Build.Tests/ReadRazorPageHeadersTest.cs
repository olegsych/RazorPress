using System;
using System.Composition.Hosting;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class ReadRazorPageHeadersTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ReadRazorPageHeaders).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolimorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(ReadRazorPageHeaders)));
        }

        [Fact]
        public void ClassIsDiscoverableDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<ReadRazorPageHeaders>();
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
            var command = new ReadRazorPageHeaders();
            command.Site = new Site { Pages = { page } };

            command.Execute();

            Assert.Equal("ExpectedValue", page.Title);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClassForPolymorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(ReadRazorPageHeaders).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsBaseMethodForConsistentErrorHandling()
        {
            var command = new ReadRazorPageHeaders();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
