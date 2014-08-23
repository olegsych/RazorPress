using Xunit;

namespace RazorPress
{
    public class PageTest
    {
        [Fact]
        public void ClassIsPublicForUsersToAccessItInTheirTemplates()
        {
            Assert.True(typeof(Page).IsPublic);
        }

        [Fact]
        public void TitleCanBeSetByUserToOverrideDefaultValueBasedOnFileName()
        {
            string title = "Hello, World";
            var page = new Page();
            page.Title = title;
            Assert.Equal(title, page.Title);
        }
    }
}
