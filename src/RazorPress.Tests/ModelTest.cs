using System.IO;
using Xunit;

namespace RazorPress
{
    public class ModelTest
    {
        [Fact]
        public void ClassIsPublicToAllowPageTemplateToBePublic()
        {
            Assert.True(typeof(Model).IsPublic);
        }

        [Fact]
        public void ConstructorInitializesPageProperty()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            var model = new Model(page);
            Assert.Same(page, model.Page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersShouldtBeAbleToReplaceIt()
        {
            Assert.Null(typeof(Model).GetProperty("Page").SetMethod);
        }
    }
}
