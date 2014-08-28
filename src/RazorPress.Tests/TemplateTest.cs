using System.IO;
using RazorEngine.Templating;
using Xunit;

namespace RazorPress
{
    public class TemplateTest
    {
        [Fact]
        public void ClassIsPublicToServeAsBaseClassOfTemplatesCreatedByUsers()
        {
            Assert.True(typeof(Template).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromTemplateBaseOfPageModelToAllowUsersAccessPageModelInTheirTemplates()
        {
            Assert.True(typeof(TemplateBase<Model>).IsAssignableFrom(typeof(Template)));
        }

        [Fact]
        public void PageReturnsPageFromModelToAllowUsersReferencingItDirectly()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            var model = new Model(page);
            var template = new Template { Model = model };
            Assert.Same(page, template.Page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersDontNeedToSetItInTemplates()
        {
            Assert.Null(typeof(Template).GetProperty("Page").SetMethod);
        }
    }
}
