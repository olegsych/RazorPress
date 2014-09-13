using System.Linq;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class TransformTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Transform).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(Transform)));
        }

        [Fact]
        public void ClassSpecifiesCommandsItDependsOnViaMefBuildAttributes()
        {
            var attribute = typeof(Transform).GetCustomAttributes(false).OfType<DependsOnAttribute>().Single();
            Assert.Equal(new[] { typeof(TransformMarkdownPages), typeof(TransformRazorPages) }, attribute.DependencyCommandTypes);
        }
    }
}
