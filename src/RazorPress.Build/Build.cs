namespace RazorPress.Build
{
    /// <summary>
    /// Defines RazorPress build process.
    /// </summary>
    [Command(DependsOn = new[] { typeof(Discover), typeof(Prepare), typeof(Transform), typeof(Deploy) })]
    public class Build : Command
    {
    }
}
