using System.Composition.Hosting;
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
        public void ConstructorInitializesCompositeCommandWithCommandsTransformDependsOn()
        {
            var transformMarkdownPages = new TransformMarkdownPages();
            var transformRazorPages = new TransformRazorPages();

            var transform = new Transform(transformMarkdownPages, transformRazorPages);

            Assert.Equal(new ICommand[] { transformMarkdownPages, transformRazorPages }, transform.DependsOn);
        }

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<Transform>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
