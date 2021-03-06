﻿using System;
using System.Composition.Hosting;
using System.Linq;
using MefBuild;
using Xunit;

namespace RazorPress.Build
{
    public class GenerateSiteTagsTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(GenerateSiteTags).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromSiteCommandAndAppliesToEntireSite()
        {
            Assert.True(typeof(SiteCommand).IsAssignableFrom(typeof(GenerateSiteTags)));
        }

        [Fact]
        public void ClassIsDiscoverableDuringComposition()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(SiteCommand).Assembly);
            CompositionHost container = configuration.CreateContainer();
            var command = container.GetExport<GenerateSiteTags>();
            Assert.NotNull(command);
        }

        [Fact]
        public void ExecuteCreatesSingleTagObjectWhenSiteHasPageWithSingleTagName()
        {
            var page = new Page("/index.html") { Tags = new[] { "TagName" } };
            var site = new Site() { Pages = { page } };
            var command = new GenerateSiteTags { Site = site };

            command.Execute();

            Tag tag = site.Tags[0];
            Assert.Equal("TagName", tag.Name);
            Assert.Same(page, tag.Pages[0]);
        }

        [Fact]
        public void ExecuteCreatesMultipleTagObjectsWhenSiteHasPageWithMultipleTagNames()
        {
            string[] tagNames = { "tag1", "tag2" };
            var page = new Page("/index.html") { Tags = tagNames };
            var site = new Site() { Pages = { page } };
            var command = new GenerateSiteTags { Site = site };

            command.Execute();

            Assert.Equal(tagNames, site.Tags.Select(t => t.Name));
            Assert.True(site.Tags.All(t => t.Pages.Contains(page)));
        }

        [Fact]
        public void ExecuteAddsPageToExistingTagWithMatchingName()
        {
            var tag = new Tag("TagName");
            var page = new Page("/index.html") { Tags = new[] { tag.Name } };
            var site = new Site() { Pages = { page }, Tags = { tag } };
            var command = new GenerateSiteTags { Site = site };

            command.Execute();

            Assert.Equal(1, site.Tags.Count);
            Assert.Same(page, tag.Pages[0]);
        }

        [Fact]
        public void ExecuteIgnoresCaseWhenComparingTagNames()
        {
            var tag = new Tag("TAGNAME");
            var page = new Page("/index.html") { Tags = new[] { "tagname" } };
            var site = new Site() { Pages = { page }, Tags = { tag } };
            var command = new GenerateSiteTags { Site = site };

            command.Execute();

            Assert.Equal(1, site.Tags.Count);
            Assert.Same(page, tag.Pages[0]);
        }

        [Fact]
        public void ExecuteOverridesInheritedMethodForPolimorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(GenerateSiteTags).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesBaseMethodToReuseErrorHandling()
        {
            var command = new GenerateSiteTags();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}
