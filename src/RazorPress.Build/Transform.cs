namespace RazorPress.Build
{
    /// <summary>
    /// Defines transformation stage of the RazorPress build process.
    /// </summary>
    [Command(DependsOn = new[] { typeof(TransformMarkdownPages), typeof(TransformRazorPages) })]
    public class Transform : Command
    {
    }
}
