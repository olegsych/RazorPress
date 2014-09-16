using System;
using System.Composition;
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
        public void ClassIsDiscoverableViaComposition()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<SavePagesToDirectory>().WithPart<Configuration>().CreateContainer();
            var command = context.GetExport<SavePagesToDirectory>();
            Assert.NotNull(command);
        }

        [Fact]
        public void TargetDirectoryIsImportedFromConfigurationDuringComposition()
        {
            CompositionContext context = new ContainerConfiguration().WithPart<SavePagesToDirectory>().WithPart<Configuration>().CreateContainer();

            var configuration = context.GetExport<Configuration>();
            configuration.TargetDirectory = new DirectoryInfo(Path.GetRandomFileName());
            var command = context.GetExport<SavePagesToDirectory>();

            Assert.Same(configuration.TargetDirectory, command.TargetDirectory);
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
            var command = new SavePagesToDirectory { TargetDirectory = new DirectoryInfo(Path.GetRandomFileName()) };
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteCreatesFileInTargetDirectory()
        {
            this.Directory.Create();

            var page = new Page("/index.html");
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { TargetDirectory = this.Directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.Directory.FullName, "index.html");
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public void ExecuteCreatesTargetDirectoryIfItDoesNotExist()
        {
            var page = new Page("/index.html");
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { TargetDirectory = this.Directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.Directory.FullName, "index.html");
            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public void ExecuteWritesPageContentToTargetFile()
        {
            var page = new Page("/index.html") { Content = "TestValue" };
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { TargetDirectory = this.Directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.Directory.FullName, "index.html");
            Assert.Equal(page.Content, File.ReadAllText(filePath));
        }

        [Fact]
        public void ExecuteWritesPageContentToFileInSubdirectoryOfTarget()
        {
            var page = new Page("/subdirectory/index.html") { Content = "TestValue" };
            var site = new Site { Pages = { page } };
            var command = new SavePagesToDirectory { TargetDirectory = this.Directory, Site = site };

            command.Execute();

            string filePath = Path.Combine(this.Directory.FullName, "subdirectory\\index.html");
            Assert.Equal(page.Content, File.ReadAllText(filePath));
        }
    }
}
