namespace RazorPress.Build
{
    /// <summary>
    /// Defines RazorPress build process.
    /// </summary>
    public class Build : CompositeCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Build"/> command.
        /// </summary>
        public Build(Discover discover, Prepare prepare, Transform transform, Deploy deploy)
            : base(new Command[] { discover, prepare, transform, deploy })
        {
        }
    }
}
