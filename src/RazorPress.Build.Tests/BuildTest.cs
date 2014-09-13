using System.Linq;
using MefBuild;
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
        public void ClassSpecifiesCommandsItDependsOnViaMefBuildAttributes()
        {
            var attribute = typeof(Build).GetCustomAttributes(false).OfType<DependsOnAttribute>().Single();
            Assert.Equal(new[] { typeof(Discover), typeof(Prepare), typeof(Transform), typeof(Deploy) }, attribute.DependencyCommandTypes);
        }
    }
}
