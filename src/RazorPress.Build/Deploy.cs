using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines deployment stage of the RazorPress build process.
    /// </summary>
    [Export, DependsOn(typeof(SavePagesToDirectory))]
    public class Deploy : SiteCommand
    {
    }
}
