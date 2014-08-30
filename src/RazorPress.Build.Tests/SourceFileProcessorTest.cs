namespace RazorPress.Build
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public class SourceFileProcessorTest : IDisposable
    {
        private DirectoryInfo directory;

        public SourceFileProcessorTest()
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
        public void ClassIsInternalAndNotMeantToBeAccessedByUsers()
        {
            Assert.False(typeof(SourceFileProcessor).IsPublic);
        }

        [Fact]
        public void ClassImplementsISiteProcessorInterfaceForCompatibilityWithOtherSiteProcessors()
        {
            Assert.True(typeof(ISiteProcessor).IsAssignableFrom(typeof(SourceFileProcessor)));
        }

        [Fact]
        public void ProcessThrowsArgumentNullExceptionToPreventUsageErrors()
        {
            var processor = new SourceFileProcessor();
            Assert.Throws<ArgumentNullException>(() => processor.Process(null));
        }

        [Fact]
        public void ProcessCreatesPageObjectsForEachFileInSiteSourceDirectory()
        {
            this.directory.Create();
            var files = new List<FileInfo>
            {
                new FileInfo(Path.Combine(this.directory.FullName, "about.cshtml")),
                new FileInfo(Path.Combine(this.directory.FullName, "index.cshtml")),
            };
            files.ForEach(file => file.Create().Dispose());
            var site = new Site(this.directory);

            var processor = new SourceFileProcessor();
            processor.Process(site);

            Assert.Equal(files.Select(f => f.FullName), site.Pages.Select(p => p.Source.FullName));
        }

        [Fact]
        public void ProcessCreatePageObjectsForFilesInSubdirectoriesOfSourceDirectory()
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

            var processor = new SourceFileProcessor();
            processor.Process(site);

            Assert.Equal(files.Select(f => f.FullName), site.Pages.Select(p => p.Source.FullName));
        }
    }
}
