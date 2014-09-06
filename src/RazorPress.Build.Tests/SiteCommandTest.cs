using System;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class SiteCommandTest
    {
        [Fact]
        public void ClassIsPublicSoThatUsersCanSomedayBuildExtensions()
        {
            Assert.True(typeof(SiteCommand).IsPublic);
        }

        [Fact]
        public void ClassIsAbstractToOnlyServeAsBaseClass()
        {
            Assert.True(typeof(SiteCommand).IsAbstract);
        }

        [Fact]
        public void ClassInheritsFromCommandToReuseCompositionAndExecutionLogic()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(SiteCommand)));
        }

        [Fact]
        public void SitePropertyIsNullByDefaultBecauseItHasToBeInitializedByCodeExecutingCommand()
        {
            var command = new TestableCommand();
            Assert.Null(command.Site);
            var site = new Site();
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
        public void ExecuteDoesNotThrowExceptionsWhenAllInputPropertiesAreInitialized()
        {
            var command = new TestableCommand();
            command.Site = new Site();
            command.Execute(); // without exceptions
        }

        [Fact]
        public void ExecuteOverridesInheritedMethodForPolymorphicExecution()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(SiteCommand).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteMethodIsVirtualForDerivedClassesToImplementActualLogic()
        {
            Assert.True(typeof(SiteCommand).GetMethod("Execute").IsVirtual);
        }

        private class TestableCommand : SiteCommand
        {
        }
    }
}
