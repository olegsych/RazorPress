using System.Linq;
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
        public void ClassInheritsFromCommandBecauseItIsACommand()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(Deploy)));
        }

        [Fact]
        public void ClassDefinesCommandsItDependsOnUsingMefMetadata()
        {
            ICommandMetadata metadata = typeof(Deploy).GetCustomAttributes(false).OfType<CommandAttribute>().Single();
            Assert.Equal(new[] { typeof(SavePagesToDirectory) }, metadata.DependsOn);
        }
    }
}
