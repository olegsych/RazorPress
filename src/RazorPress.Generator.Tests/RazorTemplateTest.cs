﻿using System.IO;
using RazorEngine.Templating;
using Xunit;

namespace RazorPress.Generator
{
    public class RazorTemplateTest
    {
        public class Class : RazorTemplateTest
        {
            [Fact]
            public void IsPublicToServeAsBaseClassOfTemplatesCreatedByUsers()
            {
                Assert.True(typeof(RazorTemplate).IsPublic);
            }

            [Fact]
            public void InheritsFromTemplateBaseOfPageModelToAllowUsersAccessPageModelInTheirTemplates()
            {
                Assert.True(typeof(TemplateBase<RazorTemplateModel>).IsAssignableFrom(typeof(RazorTemplate)));
            }
        }

        public class PageTest : RazorTemplateTest
        {
            [Fact]
            public void ReturnsPageFromModelToAllowUsersReferencingItDirectly()
            {
                var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
                var page = new Page(new FileInfo(Path.GetRandomFileName()));
                var model = new RazorTemplateModel(site, page);
                var template = new RazorTemplate { Model = model };
                Assert.Same(page, template.Page);
            }

            [Fact]
            public void IsReadOnlyBecauseUsersDontNeedToSetItInTemplates()
            {
                Assert.Null(typeof(RazorTemplate).GetProperty("Page").SetMethod);
            }
        }

        public class SiteTest : RazorTemplateTest
        {
            [Fact]
            public void ReturnsSiteFromModelToAllowUsersReferencingItDirectly()
            {
                var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
                var page = new Page(new FileInfo(Path.GetRandomFileName()));
                var model = new RazorTemplateModel(site, page);
                var template = new RazorTemplate { Model = model };
                Assert.Same(site, template.Site);
            }

            [Fact]
            public void IsReadOnlyBecauseUsersDontNeedToSetItInTemplates()
            {
                Assert.Null(typeof(RazorTemplate).GetProperty("Site").SetMethod);
            }
        }
    }
}
