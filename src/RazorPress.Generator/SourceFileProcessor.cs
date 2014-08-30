using System;
using System.IO;

namespace RazorPress.Generator
{
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
