using System.Composition;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines the discovery stage of the RazorPress build process.
    /// </summary>
    [Export]
    public class Discover : SiteCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Discover"/> command.
        /// </summary>
        [ImportingConstructor]
        public Discover(CollectSiteFiles collectSiteFiles) 
            : base (new[] { collectSiteFiles })
        {
        }

        /// <summary>
        /// Initializes a test instance of the <see cref="Discover"/> class without dependencies. 
        /// </summary>
        protected Discover()
        {
        }
    }
}
