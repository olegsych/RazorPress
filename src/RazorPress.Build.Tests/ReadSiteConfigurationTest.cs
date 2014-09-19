using System;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class ReadSiteConfigurationTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ReadSiteConfiguration).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandToParticipateInRazorPressBuildProcess()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(ReadSiteConfiguration)));
        }

        [Fact]
        public void ClassIsExportedAsSharedSoThatConfigurationFileIsReadOnlyOnce()
        {
            CompositionContext context = new ContainerConfiguration()
                .WithParts(typeof(ReadSiteConfiguration), typeof(Configuration))
                .CreateContainer();

            var one = context.GetExport<ReadSiteConfiguration>();
            var two = context.GetExport<ReadSiteConfiguration>();
            
            Assert.Same(one, two);
        }

        [Fact]
        public void ConfigurationIsImportedDuringComposition()
        {
            CompositionContext context = new ContainerConfiguration()
                .WithParts(typeof(ReadSiteConfiguration), typeof(Configuration))
                .CreateContainer();

            var command = context.GetExport<ReadSiteConfiguration>();
            var configuration = context.GetExport<Configuration>();

            Assert.Same(configuration, command.Configuration);
        }

        [Fact]
        public void ConfigurationFilePointsToSiteCsConfigFileInCurrentDirectoryByDefault()
        {
            var command = new ReadSiteConfiguration();
            FileInfo configurationFile = command.ConfigurationFile;
            Assert.Equal(new FileInfo("Site.csconfig").FullName, configurationFile.FullName);
        }

        [Fact]
        public void ConfigurationFileThrowsArgumentNullExceptionToPreventUsageErrors()
        {
            var command = new ReadSiteConfiguration();
            Assert.Throws<ArgumentNullException>(() => command.ConfigurationFile = null);
        }

        [Fact]
        public void ExecuteDoesNothingIfConfigurationFileDoesNotExist()
        {
            var configuration = new Configuration();
            var command = new ReadSiteConfiguration { Configuration = configuration };

            command.Execute();
            
            Assert.Same(configuration, command.Configuration);
        }

        [Fact]
        public void ExecuteUpdatesConfigurationUsingConfigurationTemplateFileIfItExists()
        {
            string configurationFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(configurationFile, "@{ TargetDirectory = new DirectoryInfo(\"TestDir\"); }");

                var configuration = new Configuration();

                var command = new ReadSiteConfiguration();
                command.Configuration = configuration;
                command.ConfigurationFile = new FileInfo(configurationFile);
                command.Execute();

                Assert.Equal("TestDir", configuration.TargetDirectory.Name);
            }
            finally
            {
                File.Delete(configurationFile);
            }
        }

        [Fact]
        public void ExecutePreservesConfigurationPropertiesNotSpecifiedInConfigurationFile()
        {
            string configurationFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(configurationFile, "@{}");

                var testDirectory = new DirectoryInfo("TestDir");
                var configuration = new Configuration { TargetDirectory = testDirectory };

                var command = new ReadSiteConfiguration();
                command.Configuration = configuration;
                command.ConfigurationFile = new FileInfo(configurationFile);
                command.Execute();

                Assert.Equal(testDirectory, configuration.TargetDirectory);
            }
            finally
            {
                File.Delete(configurationFile);
            }
        }

        [Fact]
        public void ExecuteThrowsInvalidOperationExceptionWhenConfigurationIsNullToPreventUsageErrors()
        {
            var command = new ReadSiteConfiguration();
            var e = Assert.Throws<InvalidOperationException>(() => command.Execute());
            Assert.Contains("Configuration", e.Message);
        }
    }
}
