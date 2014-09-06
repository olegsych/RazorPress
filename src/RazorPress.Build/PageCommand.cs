using System;

namespace RazorPress.Build
{
    /// <summary>
    /// Serves as a base class for commands process a single <see cref="Page"/>.
    /// </summary>
    public abstract class PageCommand : SiteCommand
    {
        /// <summary>
        /// Executes the page command.
        /// </summary>
        /// <exception cref="InvalidOperationException">The <see cref="Site"/> is null.</exception>
        public override void Execute()
        {
            base.Execute();

            foreach (Page page in this.Site.Pages)
            {
                this.Execute(page);
            }
        }

        /// <summary>
        /// Overriden in derived classes to perform command logic for every <see cref="Page"/> of the <see cref="Site"/>.
        /// </summary>
        protected abstract void Execute(Page page);
    }
}
