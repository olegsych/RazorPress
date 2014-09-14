using System.Collections.Generic;
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
        }

        public class Pages : SiteTest
        {
            [Fact]
            public void PagesIsNotNullByDefaultToPreventUsageErrors()
            {
                var site = new Site();
                IList<Page> pages = site.Pages;
                Assert.NotNull(pages);
            }

            [Fact]
            public void PagesIsReadOnlyBecauseListItReturnsCanBeModified()
            {
                Assert.Null(typeof(Site).GetProperty("Pages").SetMethod);
            }
        }

        public class Tags : SiteTest
        {
            [Fact]
            public void TagsIsNotNullByDefaultToPreventUsageErrors()
            {
                var site = new Site();
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
