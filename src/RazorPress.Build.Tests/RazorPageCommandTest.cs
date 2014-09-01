using System;
using System.IO;
using System.Reflection;
using RazorEngine.Templating;
using Xunit;

namespace RazorPress.Build
{
    public class RazorPageCommandTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(RazorPageCommand).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromPageCommandAndMeantForPageProcessing()
        {
            Assert.True(typeof(PageCommand).IsAssignableFrom(typeof(RazorPageCommand)));
        }

        [Fact]
        public void ClassIsAbstractAndServesOnlyAsBaseClass()
        {
            Assert.True(typeof(RazorPageCommand).IsAbstract);
        }

        [Fact]
        public void TransformIsProtectedMeantToBeUsedOnlyByDerivedClasses()
        {
            Assert.True(typeof(RazorPageCommand).GetMethod("Transform", BindingFlags.Instance | BindingFlags.NonPublic).IsFamily);
        }

        [Fact]
        public void TransformReturnsTransformedTemplate()
        {
            var page = new Page("/index.html");
            var command = new TestableRazorPageCommand();
            command.Site = new Site { Pages = { page } };

            string output = command.Transform(page, "@DateTime.Now.Year");

            Assert.Equal(DateTime.Now.Year.ToString(), output);
        }

        [Fact]
        public void TransformPassesSiteObjectToRazorTemplate()
        {
            var command = new TestableRazorPageCommand { Site = new Site() };
            var page = new Page("/index.html");

            string output = command.Transform(page, "@this.Site.GetHashCode().ToString()");

            Assert.Equal(command.Site.GetHashCode().ToString(), output);
        }

        [Fact]
        public void TransformPassesPageObjectToRazorTemplate()
        {
            var command = new TestableRazorPageCommand { Site = new Site() };
            var page = new Page("/index.html");

            string output = command.Transform(page, "@this.Page.GetHashCode().ToString()");

            Assert.Equal(page.GetHashCode().ToString(), output);
        }

        private class TestableRazorPageCommand : RazorPageCommand
        {
            public new string Transform(Page page, string template)
            {
                return base.Transform(page, template);
            }

            protected override void Execute(Page page)
            {
            }
        }
    }
}
