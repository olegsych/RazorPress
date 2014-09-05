using System.Linq;
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
        public void ClassInheritsFromCompositeCommandToReuseCommonExecutionLogic()
        {
            Assert.True(typeof(CompositeCommand).IsAssignableFrom(typeof(Prepare)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsPrepareDependsOn()
        {
            var executeRazorPageHeaders = new ExecuteRazorPageHeaders();
            var generateSiteTags = new GenerateSiteTags();

            var prepare = new Prepare(executeRazorPageHeaders, generateSiteTags);

            Assert.Equal(new Command[] { executeRazorPageHeaders, generateSiteTags}, prepare.DependsOn);
        }
    }
}
