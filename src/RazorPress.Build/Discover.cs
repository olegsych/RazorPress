namespace RazorPress.Build
{
    /// <summary>
    /// Defines the discovery stage of the RazorPress build process.
    /// </summary>
    [Command(DependsOn = new[] { typeof(CollectSiteFiles) })]
    public class Discover : Command
    {
    }
}
