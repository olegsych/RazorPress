using System;
using MefBuild;
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
        public void ClassInheritsFromSiteCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(PageCommand)));
        }

        [Fact]
        public void ClassIsAbstractToBeUsedOnlyAsBaseClass()
        {
            Assert.True(typeof(PageCommand).IsAbstract);
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
        public void ExecuteInvokesProtectedExecuteMethodForEveryPageOfSite()
        {
            var testPage = new Page("/index.html");
            Page executedPage = null;
            var command = new TestablePageCommand();
            command.OnExecute = page => executedPage = page;
            command.Site = new Site { Pages = { testPage } };

            command.Execute();

            Assert.Same(testPage, executedPage);
        }

        private class TestablePageCommand : PageCommand
        {
            public TestablePageCommand()
            {
                this.OnExecute = page => { };
            }

            public Action<Page> OnExecute { get; set; }

            protected override void Execute(Page page)
            {
                this.OnExecute(page);
            }
        }
    }
}
