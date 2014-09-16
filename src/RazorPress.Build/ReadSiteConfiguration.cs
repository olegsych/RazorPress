using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    [Shared, Export]
    public class ReadSiteConfiguration : Command
    {
        [Import]
        public Configuration Configuration { get; set; }
    }
}
