namespace RazorPress.Build
{
    /// <summary>
    /// Defines deployment stage of the RazorPress build process.
    /// </summary>
    public class Deploy : CompositeCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deploy"/> command.
        /// </summary>
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
