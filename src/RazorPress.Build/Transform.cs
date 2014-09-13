using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines transformation stage of the RazorPress build process.
    /// </summary>
    [Export, DependsOn(typeof(TransformMarkdownPages), typeof(TransformRazorPages))]
    public class Transform : SiteCommand
    {
    }
}
