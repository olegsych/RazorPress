using System.Linq;
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
        public void ClassInheritsFromCommandForPolymorphism()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(Transform)));
        }

        [Fact]
        public void ClassDefinesCommandsItDependsOnThroughMefMetadata()
        {
            ICommandMetadata metadata = typeof(Transform).GetCustomAttributes(false).OfType<CommandAttribute>().Single();
            Assert.Equal(new[] { typeof(TransformMarkdownPages), typeof(TransformRazorPages) }, metadata.DependsOn);
        }
    }
}
