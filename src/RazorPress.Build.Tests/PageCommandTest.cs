using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class PageCommandTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(PageCommand).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(PageCommand)));
        }

        [Fact]
        public void ClassIsAbstractToBeUsedOnlyAsBaseClass()
        {
            Assert.True(typeof(PageCommand).IsAbstract);
        }

        [Fact]
        public void PagePropertyIsNullByDefaultAndHasToBeInitializedByCodeExecutingTheCommand()
        {
            var command = new TestablePageCommand();
            Assert.Null(command.Page);
            var page = new Page("index.html");
            command.Page = page;
            Assert.Same(page, command.Page);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(PageCommand).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var command = new TestablePageCommand();
            var e = Assert.Throws<InvalidOperationException>(() => command.Execute());
            Assert.Contains("Site", e.Message);
        }

        [Fact]
        public void ExecuteThrowsInvalidOperationExceptionWhenPageIsNullToPreventUsageErrors()
        {
            var command = new TestablePageCommand();
            command.Site = new Site();
            var e = Assert.Throws<InvalidOperationException>(() => command.Execute());
            Assert.Contains("Page", e.Message);
        }

        [Fact]
        public void ExecuteDoesNotThrowExceptionsWhenAllInputPropertiesAreInitialized()
        {
            var command = new TestablePageCommand();
            command.Site = new Site();
            command.Page = new Page("index.html");
            command.Execute(); // without exceptions
        }

        private class TestablePageCommand : PageCommand
        {
        }
    }
}
