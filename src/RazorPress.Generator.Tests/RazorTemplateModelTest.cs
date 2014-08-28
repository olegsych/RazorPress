using System.IO;
using Xunit;

namespace RazorPress.Generator
{
    public class RazorTemplateModelTest
    {
        [Fact]
        public void ClassIsPublicToAllowPageTemplateToBePublic()
        {
            Assert.True(typeof(RazorTemplateModel).IsPublic);
        }

        [Fact]
        public void ConstructorInitializesPageProperty()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            var model = new RazorTemplateModel(page);
            Assert.Same(page, model.Page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersShouldtBeAbleToReplaceIt()
        {
            Assert.Null(typeof(RazorTemplateModel).GetProperty("Page").SetMethod);
        }
    }
}
