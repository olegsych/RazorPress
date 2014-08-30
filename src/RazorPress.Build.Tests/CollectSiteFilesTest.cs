namespace RazorPress.Build
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public class CollectSiteFilesTest : IDisposable
    {
        private DirectoryInfo directory;

        public CollectSiteFilesTest()
        {
            this.directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
        }

        public void Dispose()
        {
            if (this.directory.Exists)
            {
                this.directory.Delete(true);
            }
        }

        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(CollectSiteFiles).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandForCompatibilityWithOtherSiteProcessors()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(CollectSiteFiles)));
        }

        [Fact]
        public void ExecuteInvokesBaseMethodForConsistentErrorHandling()
        {
            var processor = new CollectSiteFiles();
            Assert.Throws<InvalidOperationException>(() => processor.Execute());
        }

        [Fact]
        public void ExecuteCreatesPageObjectsForEachFileInSiteSourceDirectory()
        {
            this.directory.Create();
            var files = new List<FileInfo>
            {
                new FileInfo(Path.Combine(this.directory.FullName, "about.cshtml")),
                new FileInfo(Path.Combine(this.directory.FullName, "index.cshtml")),
            };
            files.ForEach(file => file.Create().Dispose());
            var site = new Site(this.directory);

            var processor = new CollectSiteFiles { Site = site };
            processor.Execute();

            Assert.Equal(files.Select(f => f.FullName), site.Pages.Select(p => p.Source.FullName));
        }

        [Fact]
        public void ExecuteCreatePageObjectsForFilesInSubdirectoriesOfSourceDirectory()
        {
            this.directory.Create();
            DirectoryInfo subDirectory = this.directory.CreateSubdirectory("SubDirectory");
            var files = new List<FileInfo>
            {
                new FileInfo(Path.Combine(subDirectory.FullName, "page1.cshtml")),
                new FileInfo(Path.Combine(subDirectory.FullName, "page2.cshtml")),
            };
            files.ForEach(file => file.Create().Dispose());
            var site = new Site(this.directory);

            var processor = new CollectSiteFiles { Site = site };
            processor.Execute();

            Assert.Equal(files.Select(f => f.FullName), site.Pages.Select(p => p.Source.FullName));
        }
    }
}
