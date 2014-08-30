using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class TransformRazorPageTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(TransformRazorPage).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromPageCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(PageCommand).IsAssignableFrom(typeof(TransformRazorPage)));
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClass()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(TransformRazorPage).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsInheritedMethodForConsistentErrorHandling()
        {
            var command = new TransformRazorPage();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }

        [Fact]
        public void ExecuteTransformsContentOfGivenPage()
        {
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Title = "Hello, World";
            page.Content = "@Model.Page.Title";

            var command = new TransformRazorPage();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = page;
            command.Execute();
            
            Assert.Equal(page.Title, page.Content);
        }

        [Fact]
        public void ExecuteDoesNothingWithAnEmptyContentBecauseUserMayCreateAnEmptyFile()
        {
            var command = new TransformRazorPage();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = new Page(new FileInfo(Path.GetRandomFileName()));

            command.Execute();

            Assert.Equal(string.Empty, command.Page.Content);
        }
       
        [Fact]
        public void ExecutePassesSiteObjectToRazorTemplate()
        {
            var command = new TransformRazorPage();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = new Page(new FileInfo(Path.GetRandomFileName()))
            {
                Content = "@this.Site.GetHashCode().ToString()"
            };

            command.Execute();

            Assert.Equal(command.Site.GetHashCode().ToString(), command.Page.Content);
        }

        [Fact]
        public void ExecutePassesPageObjectToRazorTemplate()
        {
            var command = new TransformRazorPage();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = new Page(new FileInfo(Path.GetRandomFileName()))
            {
                Content = "@this.Page.GetHashCode().ToString()"
            };

            command.Execute();

            Assert.Equal(command.Page.GetHashCode().ToString(), command.Page.Content);
        }

        [Fact]
        public void ExecuteAllowsSettingPropertiesOfPageTemplateInTemplateCode()
        {
            const string expectedPageTitle = "Test Page Title";
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
            page.Title = "Test Page Title";
            page.Content = "@{ this.Page.Title = \"" + expectedPageTitle + "\"; }";

            var command = new TransformRazorPage();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = page;

            command.Execute();

            Assert.Equal(expectedPageTitle, page.Title);
        }
    }
}
