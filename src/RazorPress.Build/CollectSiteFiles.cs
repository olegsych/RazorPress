using System;
using System.IO;

namespace RazorPress.Build
{
    /// <summary>
    /// Responsible for discovering source files in a directory and populating the <see cref="Site"/> model
    /// with <see cref="Page"/> objects.
    /// </summary>
    public class CollectSiteFiles : Command
    {
        /// <summary>
        /// Gets or sets the directory with web site source files.
        /// </summary>
        public DirectoryInfo Directory { get; set; }

        /// <summary>
        /// Populates the <see cref="Site"/> with <see cref="Page"/> objects representing files in the source directory of a web site.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            if (this.Directory == null)
            {
                throw new InvalidOperationException("Directory must be initialized.");
            }

            foreach (FileInfo file in this.Directory.GetFiles("*.*", SearchOption.AllDirectories))
            {
                this.Site.Pages.Add(new Page(file));
            }
        }
    }
}
