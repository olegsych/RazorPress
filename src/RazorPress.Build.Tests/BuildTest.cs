using System.Composition.Hosting;
using MefBuild;
using Moq;
using Xunit;

namespace RazorPress.Build
{
    public class BuildTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Build).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(Build)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsBuildDependsOn()
        {
            var discover = new Mock<Discover>().Object;
            var prepare = new Mock<Prepare>().Object;
            var transform = new Mock<Transform>().Object;
            var deploy = new Mock<Deploy>().Object;

            var command = new Build(discover, prepare, transform, deploy);

            Assert.Equal(new ICommand[] { discover, prepare, transform, deploy }, command.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<Build>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
