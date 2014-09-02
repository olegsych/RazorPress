namespace RazorPress.Build
{
    /// <summary>
    /// Defines deployment stage of the RazorPress build process.
    /// </summary>
    [Command(DependsOn = new[] { typeof(SavePagesToDirectory) })]
    public class Deploy : Command
    {
    }
}
