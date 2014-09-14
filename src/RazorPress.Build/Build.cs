using System.Composition;
using System.Diagnostics.CodeAnalysis;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines RazorPress build process.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "Temporary")]
    [Export, DependsOn(typeof(Discover), typeof(Prepare), typeof(Transform), typeof(Deploy))]
    public class Build : SiteCommand
    {
    }
}
