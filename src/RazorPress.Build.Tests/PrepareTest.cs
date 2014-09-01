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
        public void ClassInheritsFromCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(Prepare)));
        }

        [Fact]
        public void ClassDefinesListOfCommandsItDependsOnThroughMefMetadata()
        {
            ICommandMetadata metadata = typeof(Prepare).GetCustomAttributes(false).OfType<CommandAttribute>().Single();
            Assert.Equal(new[] { typeof(ExecuteRazorPageHeaders), typeof(GenerateSiteTags) }, metadata.DependsOn);
        }
    }
}
