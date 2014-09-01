using System;
using System.IO;
using Xunit;

namespace RazorPress
{
    public class PageTest
    {
        public class Class : PageTest
        {
            [Fact]
            public void IsPublicForUsersToAccessItInTheirTemplates()
            {
                Assert.True(typeof(Page).IsPublic);
            }

            [Fact]
            public void InitializesUrlProperty()
            {
                const string url ="/about.cshtml";
                var page = new Page(url);
                Assert.Equal(url, page.Url);
            }

            [Fact]
            public void InvokesUrlPropertySetterToValidateUrlValue()
            {
                Assert.ThrowsAny<ArgumentException>(() => new Page(string.Empty));
            }
        }

        public class Content : PageTest
        {
            [Fact]
            public void IsEmptyStringByDefaultToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Equal(0, page.Content.Length);
            }

            [Fact]
            public void CanBeSetByPageProcessors()
            {
                string content = "TestContent";
                var page = new Page("/index.html");
                page.Content = content;
                Assert.Equal(content, page.Content);
            }

            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Throws<ArgumentNullException>(() => page.Content = null);
            }
        }

        public class Tags : PageTest
        {
            [Fact]
            public void IsEmptyArrayByDefaultToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Equal(0, page.Tags.Length);
            }

            [Fact]
            public void CanBeSetByUserToSpecifyTagsForPageOrPost()
            {
                var page = new Page("/index.html");
                var tags = new[] { "tag1", "tag2" };
                page.Tags = tags;
                Assert.Same(tags, page.Tags);
            }

            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Throws<ArgumentNullException>(() => page.Tags = null);
            }
        }

        public class Title : PageTest
        {
            [Fact]
            public void IsEmptyStringByDefaultToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Equal(0, page.Title.Length);
            }

            [Fact]
            public void CanBeSetByUserToOverrideDefaultValueBasedOnFileName()
            {
                string title = "Hello, World";
                var page = new Page("/index.html");
                page.Title = title;
                Assert.Equal(title, page.Title);
            }

            [Fact]
            public void ThrowsArgumentNullExceptionToPreventUsageErrors()
            {
                var page = new Page("/index.html");
                Assert.Throws<ArgumentNullException>(() => page.Title = null);
            }
        }

        public class Url : PageTest
        {
            [Fact]
            public void CanBeSetByTemplateOrCommandsToChangeLocationOfGeneratedFile()
            {
                var page = new Page("/about.html");
                string url = "/about/index.cshtml";
                page.Url = url;
                Assert.Equal(url, page.Url);
            }

            [Fact]
            public void ThrowsArgumentExceptionWhenValueIsNullOrWhiteSpace()
            {
                var page = new Page("/index.html");
                Assert.Throws<ArgumentException>(() => page.Url = null);
                Assert.Throws<ArgumentException>(() => page.Url = string.Empty);
                Assert.Throws<ArgumentException>(() => page.Url = " ");
            }

            [Fact]
            public void ThrowsUriFormatExceptionWhenValueIsNotRelativeUrl()
            {
                var page = new Page("/index.html");
                Assert.Throws<UriFormatException>(() => page.Url = "http://www.bing.com");
            }

            [Fact]
            public void ThrowsUriFormatExceptionWhenValueDoesNotStartWithSlash()
            {
                var page = new Page("/index.html");
                Assert.Throws<UriFormatException>(() => page.Url = "index.html");
            }
        }
    }
}
