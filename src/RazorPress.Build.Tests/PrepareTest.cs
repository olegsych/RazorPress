using System.Composition.Hosting;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class PrepareTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Prepare).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandToReuseCommonExecutionLogic()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(Prepare)));
        }

        [Fact]
        public void ConstructorInitializesCommandWithCommandsPrepareDependsOn()
        {
            var executeRazorPageHeaders = new ExecuteRazorPageHeaders();
            var generateSiteTags = new GenerateSiteTags();

            var prepare = new Prepare(executeRazorPageHeaders, generateSiteTags);

            Assert.Equal(new ICommand[] { executeRazorPageHeaders, generateSiteTags}, prepare.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<Prepare>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
