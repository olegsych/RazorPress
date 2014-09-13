using System.Linq;
using MefBuild;
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
        public void ClassSpecifiesCommandsItDependsOnViaMefBuildAttributes()
        {
            var attribute = typeof(Discover).GetCustomAttributes(false).OfType<DependsOnAttribute>().Single();
            Assert.Equal(new[] { typeof(CollectSiteFiles) }, attribute.DependencyCommandTypes);
        }
    }
}
