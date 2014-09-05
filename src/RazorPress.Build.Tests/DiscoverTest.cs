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

        [Fact]
        public void ConstructorIsAutomaticallyInvokedDuringComposition()
        {
            var catalog = new AssemblyCatalog(typeof(Command).Assembly);
            var container = new CompositionContainer(catalog);
            var command = container.GetExportedValue<Discover>();
            Assert.NotEmpty(command.DependsOn);
        }
    }
}
