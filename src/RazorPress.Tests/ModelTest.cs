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
        public void PageIsNotNullSoThatUsersDontHaveToInitializeIt()
        {
            var model = new Model();
            Page page = model.Page;
            Assert.NotNull(page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersShouldtBeAbleToReplaceIt()
        {
            Assert.Null(typeof(Model).GetProperty("Page").SetMethod);
        }
    }
}
