using System;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using Xunit;

namespace RazorPress
{
    public class ConfigurationTest
    {
        [Fact]
        public void ClassIsPublicForUsersToUseInConfigurationFiles()
        {
            Assert.True(typeof(Configuration).IsPublic);
        }

        [Fact]
        public void ClassIsExposedViaCompositionAsSingletonForAllConsumersToAccessAndModifySingleConfigurationInstance()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
            var first = context.GetExport<Configuration>();
            var second = context.GetExport<Configuration>();
            Assert.Same(first, second);
        }

        [Fact]
        public void SourceDirectoryIsCurrentDirectoryByDefaultToPreventUsageErrors()
        {
            var configuration = new Configuration();
            DirectoryInfo sourceDirectory = configuration.SourceDirectory;
            Assert.Equal(Environment.CurrentDirectory, sourceDirectory.FullName);
        }

        [Fact]
        public void SourceDirectoryCanBeChangedByUserInConfigurationFile()
        {
            var configuration = new Configuration();
            var newDirectory = new DirectoryInfo(Path.GetRandomFileName());
            configuration.SourceDirectory = newDirectory;
            Assert.Same(newDirectory, configuration.SourceDirectory);
        }

        [Fact]
        public void SourceDirectoryIsAccessibleViaCompositionSoThatCommandsDontHaveToDependOnEntireConfiguration()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
            context.GetExport<DirectoryInfo>("Source");
        }

        [Fact]
        public void SourceDirectoryThrowsArgumentNullExceptionToPreventUsageErrors()
        {
            var configuration = new Configuration();
            Assert.Throws<ArgumentNullException>(() => configuration.SourceDirectory = null);
        }

        [Fact]
        public void TargetDirectoryHasDefaultValueToPreventUsageErrors()
        {
            var configuration = new Configuration();
            DirectoryInfo targetDirectory = configuration.TargetDirectory;
            Assert.Equal(Path.Combine(Environment.CurrentDirectory, "_site"), targetDirectory.FullName);
        }

        [Fact]
        public void TargetDirectoryCanBeChangedByUserInConfigurationFile()
        {
            var configuration = new Configuration();
            var newDirectory = new DirectoryInfo(Path.GetRandomFileName());
            configuration.TargetDirectory = newDirectory;
            Assert.Same(newDirectory, configuration.TargetDirectory);
        }

        [Fact]
        public void TargetDirectoryIsAccessibleViaCompositionSoThatCommandsDontHaveToDependOnEntireConfiguration()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
            context.GetExport<DirectoryInfo>("Target");
        }

        [Fact]
        public void TargetDirectoryThrowsArgumentNullExceptionToPreventUsageErrors()
        {
            var configuration = new Configuration();
            Assert.Throws<ArgumentNullException>(() => configuration.TargetDirectory = null);
        }
    }
}
