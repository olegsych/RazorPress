using System;
using System.Composition.Hosting;
using System.IO;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class SavePagesToDirectoryTest : FileSystemTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(SavePagesToDirectory).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromPageCommandForCodeReuse()
        {
            Assert.True(typeof(PageCommand).IsAssignableFrom(typeof(SavePagesToDirectory)));
        }

        [Fact]
        public void ClassIsDiscoverableDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<SavePagesToDirectory>();
            Assert.NotNull(command);
        }

        [Fact]
        public void ExecuteThrowsInvalidOperationExceptionWhenDirectoryIsNull()
        {
            var command = new SavePagesToDirectory { Site = new Site() };
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteOverridesInheritedMethodForCodeReuseAndPolymorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(SavePagesToDirectory).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesInheritedMethodForConsistentErrorHandling()
        {
            var command = new SavePagesToDirectory { Directory = new DirectoryInfo(Path.GetRandomFileName()) };
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteCreatesFileInTargetDirectory()
        {
            this.directory.Create();

            var page = new Page("/index.html");
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { Directory = this.directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.directory.FullName, "index.html");
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public void ExecuteCreatesTargetDirectoryIfItDoesNotExist()
        {
            var page = new Page("/index.html");
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { Directory = this.directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.directory.FullName, "index.html");
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public void ExecuteWritesPageContentToTargetFile()
        {
            var page = new Page("/index.html") { Content = "TestValue" };
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { Directory = this.directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.directory.FullName, "index.html");
            Assert.Equal(page.Content, File.ReadAllText(filePath));
        }

        [Fact]
        public void ExecuteWritesPageContentToFileInSubdirectoryOfTarget()
        {
            var page = new Page("/subdirectory/index.html") { Content = "TestValue" };
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { Directory = this.directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.directory.FullName, "subdirectory\\index.html");
            Assert.Equal(page.Content, File.ReadAllText(filePath));
        }
    }
}
