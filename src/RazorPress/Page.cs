using System;
using System.IO;

namespace RazorPress
{
    /// <summary>
    /// Represents a web page.
    /// </summary>
    public class Page
    {
        private string content = string.Empty;
        private string[] tags = new string[0];
        private string title = string.Empty;
        private Uri url;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class with the given <see cref="Url"/>.
        /// </summary>
        public Page(string url)
        {
            this.Url = url;
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
        /// Gets or sets the tags of this page.
        /// </summary>
        public string[] Tags
        {
            get { return this.tags; }
            set { SetProperty(ref this.tags, value); }
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

        /// <summary>
        /// Gets or sets the relative URL of the page without the domain.
        /// </summary>
        public string Url
        {
            get 
            { 
                return this.url.ToString(); 
            }

            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Relative url value is expected.");
                }

                if (!value.StartsWith("/"))
                {
                    throw new UriFormatException("Url must start with a slash (/).");
                }

                this.url = new Uri(value, UriKind.Relative);
            }
        }

        private static void SetProperty<T>(ref T field, T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            field = value;
        }
    }
}
