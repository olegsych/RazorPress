namespace RazorPress.Build
{
    using System;
    using System.Composition.Hosting;
    using System.IO;
    using Xunit;

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
        public void ClassIsDiscoverableDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<CollectSiteFiles>();
            Assert.NotNull(command);
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var command = new CollectSiteFiles { Directory = new DirectoryInfo(Path.GetRandomFileName()) };
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
            this.directory.Create();
            DirectoryInfo subDirectory = this.directory.CreateSubdirectory("SubDirectory");
            var file = new FileInfo(Path.Combine(subDirectory.FullName, "about.cshtml"));
            const string fileContent = "TestContent";
            File.WriteAllText(file.FullName, fileContent);
            var site = new Site();

            var processor = new CollectSiteFiles { Site = site, Directory = this.directory };
            processor.Execute();

            Assert.Equal(fileContent, site.Pages[0].Content);
        }

        [Fact]
        public void ExecuteCreatesPageObjectWithUrlSetToRelativePathToFileFromDirectory()
        {
            this.directory.Create();
            DirectoryInfo subdirectory = this.directory.CreateSubdirectory("subdirectory");
            var file = new FileInfo(Path.Combine(subdirectory.FullName, "about.cshtml"));
            file.Create().Dispose();
            var site = new Site();

            var command = new CollectSiteFiles { Site = site, Directory = this.directory };
            command.Execute();

            Assert.Equal("/subdirectory/about.cshtml", site.Pages[0].Url);
        }
    }
}
