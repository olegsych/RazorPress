using System.Linq;
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
        public void ClassSpecifiesCommandsItDependsOnViaMefBuildAttributes()
        {
            var attribute = typeof(Prepare).GetCustomAttributes(false).OfType<DependsOnAttribute>().Single();
            Assert.Equal(new[] { typeof(ExecuteRazorPageHeaders), typeof(GenerateSiteTags) }, attribute.DependencyCommandTypes);
        }
    }
}
