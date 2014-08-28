using System;
using Xunit;

namespace RazorPress
{
    public class TagTest
    {
        public class Class : TagTest
        {
            [Fact]
            public void IsPublicAndCanBeAccessedByUsers()
            {
                Assert.True(typeof(Tag).IsPublic);
            }

            [Fact]
            public void ThrowsArgumentNullExceptionWhenNameIsNullToPreventUsageErrors()
            {
                Assert.Throws<ArgumentNullException>(() => new Tag(null));
            }

            [Fact]
            public void ThrowsArgumentExceptionWhenNameIsEmptyToPreventUsageErrors()
            {
                Assert.Throws<ArgumentException>(() => new Tag(" "));
            }
        }

        public class Name : TagTest
        {
            [Fact]
            public void IsInitializedByConstructor()
            {
                var tagName = "TestTag";
                var tag = new Tag(tagName);
                Assert.Equal(tagName, tag.Name);
            }

            [Fact]
            public void IsReadOnlyAndCannotBeModified()
            {
                Assert.Null(typeof(Tag).GetProperty("Name").SetMethod);
            }
        }

        public class Pages : TagTest
        {
            [Fact]
            public void IsNotNullToPreventNullReferenceExceptions()
            {
                var tag = new Tag("tagName");
                Assert.NotNull(tag.Pages);
            }

            [Fact]
            public void IsReadOnlyBecauseListItselfCanBeModified()
            {
                Assert.Null(typeof(Tag).GetProperty("Pages").SetMethod);
            }
        }
    }
}
