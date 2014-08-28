using System;
using System.IO;
using Xunit;

namespace RazorPress.Generator
{
    public class RazorProcessorTest
    {
        [Fact]
        public void ClassIsInternalAndNotMeantToBeAccessedByUsers()
        {
            Assert.False(typeof(RazorProcessor).IsPublic);
        }

        [Fact]
        public void ProcessTransormsContentOfGivenPage()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Title = "Hello, World";
            page.Content = "@Model.Page.Title";

            var processor = new RazorProcessor();
            processor.Process(page);
            
            Assert.Equal(page.Title, page.Content);
        }

        [Fact]
        public void PageDoesNothingWithAnEmptyPageBecauseUserMayCreateAnEmptyFile()
        {
            var processor = new RazorProcessor();

            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            processor.Process(page);

            Assert.Equal(string.Empty, page.Content);
        }
       
        [Fact]
        public void ProcessUsesPageTemplateToProvideConveniencePropertiesToTemplateAuthors()
        {
            var processor = new RazorProcessor();

            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Content = "@this.GetType().BaseType.Name";
            processor.Process(page);

            Assert.Equal(typeof(RazorTemplate).Name, page.Content);
        }

        [Fact]
        public void ProcessAllowsUsingPropertiesOfPageTemplateDirectlyInTemplateCode()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Title = "Test Page Title";
            page.Content = "@this.Page.Title";

            var processor = new RazorProcessor();
            processor.Process(page);

            Assert.Equal(page.Title, page.Content);
        }

        [Fact]
        public void ProcessAllowsSettingPropertiesOfPageTemplateInTemplateCode()
        {
            var processor = new RazorProcessor();

            const string expectedPageTitle = "Test Page Title";
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Title = "Test Page Title";
            page.Content = "@{ this.Page.Title = \"" + expectedPageTitle + "\"; }";

            processor.Process(page);

            Assert.Equal(expectedPageTitle, page.Title);
        }
    }
}
