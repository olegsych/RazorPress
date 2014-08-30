using System;
using System.IO;

namespace RazorPress.Build
{
    /// <summary>
    /// Responsible for discovering source files in a directory and populating the <see cref="Site"/> model
    /// with <see cref="Page"/> objects.
    /// </summary>
    internal class SourceFileProcessor : ISiteProcessor
    {
        public void Process(Site site)
        {
            if (site == null)
            {
                throw new ArgumentNullException("site");
            }

            foreach (FileInfo file in site.Source.GetFiles("*.*", SearchOption.AllDirectories))
            {
                site.Pages.Add(new Page(file));
            }
        }
    }
}
