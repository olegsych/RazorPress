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
            var template = new Template();
            template.Model = new Model();
            Page page = template.Page;
            Assert.Same(template.Model.Page, page);
        }

        [Fact]
        public void PageIsReadOnlyBecauseUsersDontNeedToSetItInTemplates()
        {
            Assert.Null(typeof(Template).GetProperty("Page").SetMethod);
        }
    }
}
