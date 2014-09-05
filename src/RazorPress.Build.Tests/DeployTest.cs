using System.ComponentModel.Composition.Hosting;
using System.Linq;
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
        public void ClassInheritsFromCompositeCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(CompositeCommand).IsAssignableFrom(typeof(Deploy)));
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
            var catalog = new AssemblyCatalog(typeof(Command).Assembly);
            var container = new CompositionContainer(catalog);
            var command = container.GetExportedValue<Deploy>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
