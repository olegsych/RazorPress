using System;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using Xunit;

namespace RazorPress
{
    public class ConfigurationTest
    {
        public class Class
        {
            [Fact]
            public void IsPublicForUsersToUseInConfigurationFiles()
            {
                Assert.True(typeof(Configuration).IsPublic);
            }

            [Fact]
            public void IsExposedViaCompositionAsSingletonForAllConsumersToAccessAndModifySingleConfigurationInstance()
            {
                CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
                var first = context.GetExport<Configuration>();
                var second = context.GetExport<Configuration>();
                Assert.Same(first, second);
            }
        }

        public class SourceDirectory
        {
            [Fact]
            public void IsCurrentDirectoryByDefaultToPreventUsageErrors()
            {
                var configuration = new Configuration();
                DirectoryInfo sourceDirectory = configuration.SourceDirectory;
                Assert.Equal(Environment.CurrentDirectory, sourceDirectory.FullName);
            }

            [Fact]
            public void CanBeChangedByUserInConfigurationFile()
            {
                var configuration = new Configuration();
                var newDirectory = new DirectoryInfo(Path.GetRandomFileName());
                configuration.SourceDirectory = newDirectory;
                Assert.Same(newDirectory, configuration.SourceDirectory);
            }

            [Fact]
            public void IsAccessibleViaCompositionSoThatCommandsDontHaveToDependOnEntireConfiguration()
            {
                CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
                context.GetExport<DirectoryInfo>("Source");
            }

            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var configuration = new Configuration();
                Assert.Throws<ArgumentNullException>(() => configuration.SourceDirectory = null);
            }
        }

        public class TargetDirectory
        {
            [Fact]
            public void HasDefaultValueToPreventUsageErrors()
            {
                var configuration = new Configuration();
                DirectoryInfo targetDirectory = configuration.TargetDirectory;
                Assert.Equal(Path.Combine(Environment.CurrentDirectory, ".razor"), targetDirectory.FullName);
            }

            [Fact]
            public void CanBeChangedByUserInConfigurationFile()
            {
                var configuration = new Configuration();
                var newDirectory = new DirectoryInfo(Path.GetRandomFileName());
                configuration.TargetDirectory = newDirectory;
                Assert.Same(newDirectory, configuration.TargetDirectory);
            }

            [Fact]
            public void IsAccessibleViaCompositionSoThatCommandsDontHaveToDependOnEntireConfiguration()
            {
                CompositionContext context = new ContainerConfiguration().WithPart<Configuration>().CreateContainer();
                context.GetExport<DirectoryInfo>("Target");
            }

            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var configuration = new Configuration();
                Assert.Throws<ArgumentNullException>(() => configuration.TargetDirectory = null);
            }
        }

        public class Update
        {
            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var configuration = new Configuration();
                Assert.Throws<ArgumentNullException>(() => configuration.Update(null));
            }

            [Fact]
            public void UpdatesSourceDirectory()
            {
                var testDirectory = new DirectoryInfo("TestDirectory");
                var source = new Configuration { SourceDirectory = testDirectory };
                var target = new Configuration();

                source.Update(target);

                Assert.Same(testDirectory, target.SourceDirectory);
            }

            [Fact]
            public void UpdatesTargetDirectory()
            {
                var testDirectory = new DirectoryInfo("TestDirectory");
                var source = new Configuration { TargetDirectory = testDirectory };
                var target = new Configuration();

                source.Update(target);

                Assert.Same(testDirectory, target.TargetDirectory);
            }
        }
    }
}
