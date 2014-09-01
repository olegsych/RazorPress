namespace RazorPress.Build
{
    /// <summary>
    /// Defines discover stage of the RazorPress build process.
    /// </summary>
    [Command(DependsOn = new[] { typeof(CollectSiteFiles), typeof(ExecuteRazorPageHeaders) })]
    public class Discover : Command
    {
    }
}
