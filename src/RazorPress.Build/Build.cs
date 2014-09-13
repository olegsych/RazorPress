using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines RazorPress build process.
    /// </summary>
    [Export, DependsOn(typeof(Discover), typeof(Prepare), typeof(Transform), typeof(Deploy))]
    public class Build : SiteCommand
    {
    }
}
