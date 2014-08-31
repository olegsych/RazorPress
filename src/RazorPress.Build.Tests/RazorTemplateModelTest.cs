using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class RazorTemplateModelTest
    {
        [Fact]
        public void ClassIsPublicToAllowPageTemplateToBePublic()
        {
            Assert.True(typeof(RazorTemplateModel).IsPublic);
        }

        [Fact]
        public void ConstructorInitializesPropertyValues()
        {
            var site = new Site();
            var page = new Page();
            var model = new RazorTemplateModel(site, page);
            Assert.Same(site, model.Site);
            Assert.Same(page, model.Page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersShouldNotBeReplacingIt()
        {
            Assert.Null(typeof(RazorTemplateModel).GetProperty("Page").SetMethod);
        }

        [Fact]
        public void SiteIsReadOnlyBecauseUsersShouldNotBeReplacingIt()
        {
            Assert.Null(typeof(RazorTemplateModel).GetProperty("Site").SetMethod);
        }
    }
}
