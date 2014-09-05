using System.ComponentModel.Composition.Hosting;
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
        public void ClassInheritsFromCompositeCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(CompositeCommand).IsAssignableFrom(typeof(Transform)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsTransformDependsOn()
        {
            var transformMarkdownPages = new TransformMarkdownPages();
            var transformRazorPages = new TransformRazorPages();

            var transform = new Transform(transformMarkdownPages, transformRazorPages);

            Assert.Equal(new Command[] { transformMarkdownPages, transformRazorPages }, transform.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var catalog = new AssemblyCatalog(typeof(Command).Assembly);
            var container = new CompositionContainer(catalog);
            var command = container.GetExportedValue<Transform>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
