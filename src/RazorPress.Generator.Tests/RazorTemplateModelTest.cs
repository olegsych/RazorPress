﻿using System.IO;
using Xunit;

namespace RazorPress.Generator
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
            var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            var page = new Page(new FileInfo(Path.GetRandomFileName()));
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
