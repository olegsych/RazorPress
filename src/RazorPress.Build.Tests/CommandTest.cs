using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class CommandTest
    {
        [Fact]
        public void ClassIsPublicSoThatUsersCanSomedayBuildExtensions()
        {
            Assert.True(typeof(Command).IsPublic);
        }

        [Fact]
        public void ClassIsAbstractToOnlyServeAsBaseClass()
        {
            Assert.True(typeof(Command).IsAbstract);
        }

        [Fact]
        public void SitePropertyIsNullByDefaultBecauseItHasToBeInitializedByCodeExecutingCommand()
        {
            var command = new TestableCommand();
            Assert.Null(command.Site);
            var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Site = site;
            Assert.Same(site, command.Site);
        }

        [Fact]
        public void ExecuteThrowsInvalidOperationExceptionWhenSiteIsNullToPreventUsageErrors()
        {
            var command = new TestableCommand();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteMethodIsVirtualForDerivedClassesToImplementActualLogic()
        {
            Assert.True(typeof(Command).GetMethod("Execute").IsVirtual);
        }

        private class TestableCommand : Command
        {
        }
    }
}
