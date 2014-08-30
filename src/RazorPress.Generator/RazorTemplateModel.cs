﻿namespace RazorPress.Generator
{
    /// <summary>
    /// Serves as a model for <see cref="RazorTemplate"/>.
    /// </summary>
    public class RazorTemplateModel
    {
        private readonly Site site;
        private readonly Page page;

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorTemplateModel"/> class.
        /// </summary>
        public RazorTemplateModel(Site site, Page page)
        {
            this.site = site;
            this.page = page;
        }

        /// <summary>
        /// Gets a <see cref="Page"/> object that represents the web page generated by the current <see cref="RazorTemplate"/>.
        /// </summary>
        public Page Page 
        { 
            get { return this.page; } 
        }

        /// <summary>
        /// Gets a <see cref="Site"/> object that represents the web site being generated.
        /// </summary>
        public Site Site
        {
            get { return this.site; }
        }
    }
}
