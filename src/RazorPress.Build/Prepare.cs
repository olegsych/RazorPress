using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines the preparation stage of the RazorPress build process.
    /// </summary>
    [Export, DependsOn(typeof(ReadRazorPageHeaders), typeof(GenerateSiteTags))]
    public class Prepare : SiteCommand
    {
    }
}
