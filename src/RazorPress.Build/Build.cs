using System.Composition;
using MefBuild;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines RazorPress build process.
    /// </summary>
    [Export]
    public class Build : SiteCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Build"/> command.
        /// </summary>
        [ImportingConstructor]
        public Build(Discover discover, Prepare prepare, Transform transform, Deploy deploy)
            : base(new Command[] { discover, prepare, transform, deploy })
        {
        }
    }
}
