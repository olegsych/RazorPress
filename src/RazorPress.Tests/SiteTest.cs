using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace RazorPress
{
    public class SiteTest
    {
        public class Class : SiteTest
        {
            [Fact]
            public void IsPublicForCustomersToUseInTheirTemplates()
            {
                Assert.True(typeof(Site).IsPublic);
            }

            [Fact]
            public void ThrowsArgumentExceptionToPreventUsageErrors()
            {
                Assert.Throws<ArgumentNullException>(() => new Site(null));
            }
        }

        public class Pages : SiteTest
        {
            [Fact]
            public void PagesIsNotNullByDefaultToPreventUsageErrors()
            {
                var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
                IList<Page> pages = site.Pages;
                Assert.NotNull(pages);
            }

            [Fact]
            public void PagesIsReadOnlyBecauseListItReturnsCanBeModified()
            {
                Assert.Null(typeof(Site).GetProperty("Pages").SetMethod);
            }
        }

        public class Source : SiteTest
        {
            [Fact]
            public void IsInitializedByConstructor()
            {
                var source = new DirectoryInfo(Path.GetRandomFileName());
                var site = new Site(source);
                Assert.Same(source, site.Source);
            }

            [Fact]
            public void IsReadOnlyAndShouldNotBeChanged()
            {
                Assert.Null(typeof(Site).GetProperty("Source").SetMethod);
            }
        }

        public class Tags : SiteTest
        {
            [Fact]
            public void TagsIsNotNullByDefaultToPreventUsageErrors()
            {
                var site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
                IList<Tag> tags = site.Tags;
                Assert.NotNull(tags);
            }

            [Fact]
            public void TagsIsReadOnlyBecauseListItReturnsCanBeModified()
            {
                Assert.Null(typeof(Site).GetProperty("Tags").SetMethod);
            }
        }
    }
}
