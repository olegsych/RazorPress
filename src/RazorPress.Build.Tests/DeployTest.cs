using System.Linq;
using MefBuild;
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
        public void ClassSpecifiesCommandsItDependsOnViaMefBuildAttributes()
        {
            var attribute = typeof(Deploy).GetCustomAttributes(false).OfType<DependsOnAttribute>().Single();
            Assert.Equal(new[] { typeof(SavePagesToDirectory) }, attribute.DependencyCommandTypes);
        }
    }
}
