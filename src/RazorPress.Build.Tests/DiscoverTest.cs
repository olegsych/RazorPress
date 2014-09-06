using System.Composition.Hosting;
using Xunit;

namespace RazorPress.Build
{
    public class DiscoverTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Discover).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(Discover)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsDiscoverDependsOn()
        {
            var collectSiteFiles = new CollectSiteFiles();
            var discover = new Discover(collectSiteFiles);
            Assert.Equal(new[] { collectSiteFiles }, discover.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<Discover>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
