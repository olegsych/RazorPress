using System;
using System.IO;

namespace RazorPress
{
    /// <summary>
    /// Represents a web page.
    /// </summary>
    public class Page
    {
        private readonly FileInfo sourceFile;
        private string content = string.Empty;
        private string title = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page(FileInfo sourceFile)
        {
            if (sourceFile == null)
            {
                throw new ArgumentNullException("sourceFile");
            }

            this.sourceFile = sourceFile;
        }

        /// <summary>
        /// Gets or sets the page content.
        /// </summary>
        public string Content 
        {
            get { return this.content; }
            set { SetProperty(ref this.content, value); }
        }

        /// <summary>
        /// Gets the source file of this page.
        /// </summary>
        public FileInfo SourceFile
        {
            get { return this.sourceFile; }
        }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <remarks>
        /// Default page is automatically derived from the template file name. It can also be changed in template code.
        /// </remarks>
        public string Title 
        {
            get { return this.title; }
            set { SetProperty(ref this.title, value); }
        }

        private static void SetProperty(ref string field, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            field = value;
        }
    }
}
