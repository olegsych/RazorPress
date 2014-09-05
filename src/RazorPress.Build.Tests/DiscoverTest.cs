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
        public void ClassInheritsFromCompositeCommandToReuseCommandExecutionLogic()
        {
            Assert.True(typeof(CompositeCommand).IsAssignableFrom(typeof(Discover)));
        }

        [Fact]
        public void ConstructorInitializesCompositeCommandWithCommandsDiscoverDependsOn()
        {
            var collectSiteFiles = new CollectSiteFiles();
            var discover = new Discover(collectSiteFiles);
            Assert.Equal(new[] { collectSiteFiles }, discover.DependsOn);
        }
    }
}
