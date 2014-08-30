using System;

namespace RazorPress.Build
{
    /// <summary>
    /// Serves as a base class for commands process a single <see cref="Page"/>.
    /// </summary>
    public abstract class PageCommand : Command
    {
        /// <summary>
        /// Gets or sets the <see cref="Page"/> this command will process.
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// Executes the page command.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The <see cref="Site"/> is null -or- the <see cref="Page"/> is null.
        /// </exception>
        /// <remarks>
        /// Override this method in derived classes to implement the actual command logic. Call the 
        /// base method in the beginning of the overridden method for consistent error handling.
        /// </remarks>
        public override void Execute()
        {
            base.Execute();

            if (this.Page == null)
            {
                throw new InvalidOperationException("Page property must be initialized.");
            }
        }
    }
}
