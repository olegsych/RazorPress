using System;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class CollectSiteFilesTest : FileSystemTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(CollectSiteFiles).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandForCompatibilityWithOtherSiteProcessors()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(CollectSiteFiles)));
        }

        [Fact]
        public void ClassIsDiscoverableViaComposition()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<CollectSiteFiles>().WithPart<Configuration>().CreateContainer();
            var command = context.GetExport<CollectSiteFiles>();
            Assert.NotNull(command);
        }

        [Fact]
        public void SourceDirectoryIsImportedFromConfigurationDuringComposition()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<CollectSiteFiles>().WithPart<Configuration>().CreateContainer();           

            var configuration = context.GetExport<Configuration>();
            configuration.SourceDirectory = new DirectoryInfo(Path.GetRandomFileName());
            var command = context.GetExport<CollectSiteFiles>();
            
            Assert.Same(configuration.SourceDirectory, command.SourceDirectory);
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var command = new CollectSiteFiles { SourceDirectory = new DirectoryInfo(Path.GetRandomFileName()) };
            var e = Assert.Throws<InvalidOperationException>(() => command.Execute());
            Assert.Contains("Site", e.Message);
        }

        [Fact]
        public void ExecuteThrowsInvalidOperationExceptionWhenDirectoryIsNotInitialized()
        {
            var command = new CollectSiteFiles { Site = new Site() };
            var e = Assert.Throws<InvalidOperationException>(() => command.Execute());
            Assert.Contains("Directory", e.Message);
        }

        [Fact]
        public void ExecuteCreatesPageObjectWithContentsLoadedFromFileInSubDirectory()
        {
            this.Directory.Create();
            DirectoryInfo subDirectory = this.Directory.CreateSubdirectory("SubDirectory");
            var file = new FileInfo(Path.Combine(subDirectory.FullName, "about.cshtml"));
            const string FileContent = "TestContent";
            File.WriteAllText(file.FullName, FileContent);
            var site = new Site();

            var processor = new CollectSiteFiles { Site = site, SourceDirectory = this.Directory };
            processor.Execute();

            Assert.Equal(FileContent, site.Pages[0].Content);
        }

        [Fact]
        public void ExecuteCreatesPageObjectWithUrlSetToRelativePathToFileFromDirectory()
        {
            this.Directory.Create();
            DirectoryInfo subdirectory = this.Directory.CreateSubdirectory("subdirectory");
            var file = new FileInfo(Path.Combine(subdirectory.FullName, "about.cshtml"));
            file.Create().Dispose();
            var site = new Site();

            var command = new CollectSiteFiles { Site = site, SourceDirectory = this.Directory };
            command.Execute();

            Assert.Equal("/subdirectory/about.cshtml", site.Pages[0].Url);
        }
    }
}
