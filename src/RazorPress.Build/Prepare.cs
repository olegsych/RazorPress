namespace RazorPress.Build
{
    /// <summary>
    /// Defines the preparation stage of the RazorPress build process.
    /// </summary>
    [Command(
        DependsOn = new[] { 
            typeof(ExecuteRazorPageHeaders), 
            typeof(GenerateSiteTags) 
        }
    )]
    public class Prepare : Command
    {
    }
}
