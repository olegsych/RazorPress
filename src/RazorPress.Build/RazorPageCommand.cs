using System;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Build
{
    /// <summary>
    /// Serves as a base class for page commands that require Razor template services.
    /// </summary>
    public abstract class RazorPageCommand : PageCommand, IDisposable
    {
        private readonly TemplateService templateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RazorPageCommand"/> class.
        /// </summary>
        protected RazorPageCommand()
        {
            var configuration = new TemplateServiceConfiguration();
            configuration.BaseTemplateType = typeof(RazorTemplate);
            this.templateService = new TemplateService(configuration);
        }

        /// <summary>
        /// Disposes <see cref="TemplateService"/> created by this object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes resources owned by this instance.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.templateService.Dispose();
            }
        }

        /// <summary>
        /// Returns output of the specified <paramref name="template"/> transformed by the Razor engine.
        /// </summary>
        protected string Transform(Page page, string template)
        {
            var model = new RazorTemplateModel(this.Site, page);
            return this.templateService.Parse(template, model, null, null);
        }
    }
}
