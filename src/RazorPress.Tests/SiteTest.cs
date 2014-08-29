using System.Collections.Generic;
using Xunit;

namespace RazorPress
{
    public class SiteTest
    {
        [Fact]
        public void ClassIsPublicForCustomersToUseInTheirTemplates()
        {
            Assert.True(typeof(Site).IsPublic);
        }

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
