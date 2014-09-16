using System.Composition;
using System.Composition.Hosting;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class ReadSiteConfigurationTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ReadSiteConfiguration).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandToParticipateInRazorPressBuildProcess()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(ReadSiteConfiguration)));
        }

        [Fact]
        public void ClassIsExportedAsSharedSoThatConfigurationFileIsReadOnlyOnce()
        {
            CompositionContext context = new ContainerConfiguration()
                .WithParts(typeof(ReadSiteConfiguration), typeof(Configuration))
                .CreateContainer();

            var one = context.GetExport<ReadSiteConfiguration>();
            var two = context.GetExport<ReadSiteConfiguration>();
            
            Assert.Same(one, two);
        }

        [Fact]
        public void ConfigurationIsImportedDuringComposition()
        {
            CompositionContext context = new ContainerConfiguration()
                .WithParts(typeof(ReadSiteConfiguration), typeof(Configuration))
                .CreateContainer();

            var command = context.GetExport<ReadSiteConfiguration>();
            var configuration = context.GetExport<Configuration>();

            Assert.Same(configuration, command.Configuration);
        }

        // TODO: Test Execute "reads" Site.csconfig in current directory
        // TODO: Test Execute does nothing if current directory does not contain Site.csconfig
    }
}
