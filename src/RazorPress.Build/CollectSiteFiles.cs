using System;
using System.Composition;
using System.IO;

namespace RazorPress.Build
{
    /// <summary>
    /// Responsible for discovering source files in a directory and populating the <see cref="Site"/> model
    /// with <see cref="Page"/> objects.
    /// </summary>
    [Export]
    public class CollectSiteFiles : SiteCommand
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

            var directoryUri = new Uri(this.Directory.FullName + Path.DirectorySeparatorChar);
            foreach (FileInfo file in this.Directory.GetFiles("*.*", SearchOption.AllDirectories))
            {
                var fileUri = new Uri(file.FullName);
                Uri pageUri = directoryUri.MakeRelativeUri(fileUri);
                var page = new Page('/' + pageUri.ToString());
                page.Content = File.ReadAllText(file.FullName);
                this.Site.Pages.Add(page);
            }
        }
    }
}
