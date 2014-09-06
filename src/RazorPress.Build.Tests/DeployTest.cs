using System.Composition.Hosting;
using Xunit;

namespace RazorPress.Build
{
    public class DeployTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Deploy).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(Deploy)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandCommandsDeployDependsOn()
        {
            var savePagesToDirectory = new SavePagesToDirectory();
            var deploy = new Deploy(savePagesToDirectory);
            Assert.Equal(new[] { savePagesToDirectory }, deploy.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<Deploy>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
