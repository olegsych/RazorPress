using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Xunit;

namespace RazorPress.Build
{
    public class DiscoverTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(Discover).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandForCodeReuseAndPolymorphism()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(Discover)));
        }

        [Fact]
        public void ClassDefinesListOfCommandsItDependsOnThroughMefMetadata()
        {
           ICommandMetadata metadata = typeof(Discover).GetCustomAttributes(false).OfType<CommandAttribute>().Single();
           Assert.Equal(new[] { typeof(CollectSiteFiles), typeof(ExecuteRazorPageHeaders) }, metadata.DependsOn);
        }
    }
}
