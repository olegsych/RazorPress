using System.Composition;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines deployment stage of the RazorPress build process.
    /// </summary>
    [Export]
    public class Deploy : SiteCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deploy"/> command.
        /// </summary>
        [ImportingConstructor]
        public Deploy(SavePagesToDirectory savePagesToDirectory)
            : base(new[] { savePagesToDirectory })
        {
        }

        /// <summary>
        /// Initializes a test instance of the <see cref="Deploy"/> class without dependencies. 
        /// </summary>
        protected Deploy()
        {
        }
    }
}
