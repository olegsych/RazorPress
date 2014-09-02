using System.Linq;
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
        public void ClassInheritsFromCommandForPolymorphism()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(Build)));
        }

        [Fact]
        public void ClassDefinesCommandsItDependsOnThroughMefMetadata()
        {
            ICommandMetadata metadata = typeof(Build).GetCustomAttributes(false).OfType<CommandAttribute>().Single();
            Assert.Equal(new[] { typeof(Discover), typeof(Prepare), typeof(Transform), typeof(Deploy) }, metadata.DependsOn);
        }
    }
}
