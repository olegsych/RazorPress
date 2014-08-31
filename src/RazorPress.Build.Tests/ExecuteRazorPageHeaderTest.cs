using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class ExecuteRazorPageHeaderTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ExecuteRazorPageHeader).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolimorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(ExecuteRazorPageHeader)));
        }

        [Fact]
        public void ExecuteRendersHeaderSectionOfRazorTemplateToInitializePageProperties()
        {
            var command = new ExecuteRazorPageHeader();
            command.Site = new Site();
            command.Page = new Page(new FileInfo(Path.GetRandomFileName()))
            {
                Content = @"
                @{
                    this.Page.Title = ""ExpectedValue"";
                }"
            };

            command.Execute();

            Assert.Equal("ExpectedValue", command.Page.Title);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClassForPolymorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(ExecuteRazorPageHeader).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsBaseMethodForConsistentErrorHandling()
        {
            var command = new ExecuteRazorPageHeader();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
