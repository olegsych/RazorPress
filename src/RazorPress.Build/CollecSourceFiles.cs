using System;
using System.IO;

namespace RazorPress.Build
{
    /// <summary>
    /// Responsible for discovering source files in a directory and populating the <see cref="Site"/> model
    /// with <see cref="Page"/> objects.
    /// </summary>
    public class CollecSourceFiles : Command
    {
        /// <summary>
        /// Populates the <see cref="Site"/> with <see cref="Page"/> objects representing files in the source directory of a web site.
        /// </summary>
        public override void Execute()
        {
            base.Execute();
            foreach (FileInfo file in this.Site.Source.GetFiles("*.*", SearchOption.AllDirectories))
            {
                this.Site.Pages.Add(new Page(file));
            }
        }
    }
}
