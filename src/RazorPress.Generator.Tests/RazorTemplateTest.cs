using System.IO;
using RazorEngine.Templating;
using Xunit;

namespace RazorPress.Generator
{
    public class RazorTemplateTest
    {
        [Fact]
        public void ClassIsPublicToServeAsBaseClassOfTemplatesCreatedByUsers()
        {
            Assert.True(typeof(RazorTemplate).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromTemplateBaseOfPageModelToAllowUsersAccessPageModelInTheirTemplates()
        {
            Assert.True(typeof(TemplateBase<RazorTemplateModel>).IsAssignableFrom(typeof(RazorTemplate)));
        }

        [Fact]
        public void PageReturnsPageFromModelToAllowUsersReferencingItDirectly()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            var model = new RazorTemplateModel(page);
            var template = new RazorTemplate { Model = model };
            Assert.Same(page, template.Page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersDontNeedToSetItInTemplates()
        {
            Assert.Null(typeof(RazorTemplate).GetProperty("Page").SetMethod);
        }
    }
}
