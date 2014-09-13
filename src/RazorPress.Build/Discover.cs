using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines the discovery stage of the RazorPress build process.
    /// </summary>
    [Export, DependsOn(typeof(CollectSiteFiles))]
    public class Discover : SiteCommand
    {
    }
}
