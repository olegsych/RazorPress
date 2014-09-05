using System.Linq;
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
        public void ClassInheritsFromCompositeCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(CompositeCommand).IsAssignableFrom(typeof(Build)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsBuildDependsOn()
        {
            var discover = new Mock<Discover>().Object;
            var prepare = new Mock<Prepare>().Object;
            var transform = new Mock<Transform>().Object;
            var deploy = new Mock<Deploy>().Object;

            var command = new Build(discover, prepare, transform, deploy);

            Assert.Equal(new Command[] { discover, prepare, transform, deploy }, command.DependsOn);
        }
    }
}
