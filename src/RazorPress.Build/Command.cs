using System;

namespace RazorPress.Build
{
    /// <summary>
    /// Defines a command, which is a unit of executable code used by RazorPress to generate a web site.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Gets or sets a <see cref="Site"/> object that represents the web site being generated.
        /// </summary>
        public Site Site { get; set; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <exception cref="InvalidOperationException">The <see cref="Site"/> is null.</exception>
        /// <remarks>
        /// Override this method in derived classes to implement the actual command logic. Call the 
        /// base method in the beginning of the overridden method for consistent error handling.
        /// </remarks>
        public virtual void Execute()
        {
            if (this.Site == null)
            {
                throw new InvalidOperationException("Site property must be initalized.");
            }
        }
    }
}
