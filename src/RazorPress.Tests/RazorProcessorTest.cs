using System;
using Xunit;

namespace RazorPress
{
    public class RazorProcessorTest
    {
        [Fact]
        public void ClassIsInternalAndNotMeantToBeAccessedByUsers()
        {
            Assert.False(typeof(RazorProcessor).IsPublic);
        }

        [Fact]
        public void RenderTransormsRazorTemplateWithTheSpecifiedModel()
        {
            var model = new Model();
            model.Page.Title = "Hello, World";

            var processor = new RazorProcessor();
            string output = processor.Render("@Model.Page.Title", model);
            
            Assert.Equal(model.Page.Title, output);
        }

        [Fact]
        public void RenderUsesPageTemplateToProvideConveniencePropertiesToTemplateAuthors()
        {
            var processor = new RazorProcessor();

            string output = processor.Render("@this.GetType().BaseType.Name", new Model());

            Assert.Equal(typeof(Template).Name, output);
        }

        [Fact]
        public void RenderAllowsUsingPropertiesOfPageTemplateDirectlyInTemplateCode()
        {
            var model = new Model();
            model.Page.Title = "Test Page Title";

            var processor = new RazorProcessor();
            string output = processor.Render("@this.Page.Title", model);

            Assert.Equal(model.Page.Title, output);
        }

        [Fact]
        public void RenderAllowsSettingPropertiesOfPageTemplateInTemplateCode()
        {
            var processor = new RazorProcessor();

            const string expectedPageTitle = "Test Page Title";
            var model = new Model();
            processor.Render("@{ this.Page.Title = \"" + expectedPageTitle + "\"; }", model);

            Assert.Equal(expectedPageTitle, model.Page.Title);
        }
    }
}
