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
           ICommandMetadata metadata = GetCommandMetadata<Discover>();
           Assert.Equal(new[] { typeof(CollectSiteFiles), typeof(ExecuteRazorPageHeaders) }, metadata.DependsOn);
        }

        private static ICommandMetadata GetCommandMetadata<T>() where T : Command
        {
            using (var catalog = new AssemblyCatalog(typeof(T).Assembly))
            using (var container = new CompositionContainer(catalog))
            {
                IEnumerable<Lazy<Command, ICommandMetadata>> exports = container.GetExports<Command, ICommandMetadata>(string.Empty);
                Lazy<Command, ICommandMetadata> export = exports.Single(e => e.Value is T);
                return export.Metadata;
            }
        }
    }
}
