using System.IO;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms Razor template.
    /// </summary>
    public class TransformRazorPage : PageCommand
    {
        private readonly TemplateServiceConfiguration configuration;
        private readonly TemplateService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformRazorPage"/> class.
        /// </summary>
        public TransformRazorPage()
        {
            this.configuration = new TemplateServiceConfiguration();
            this.configuration.BaseTemplateType = typeof(RazorTemplate);
            this.service = new TemplateService(configuration);
        }

        /// <summary>
        /// Transforms <see cref="Page.Content"/> using Razor templating engine.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            string template = this.Page.Content;
            if (!string.IsNullOrEmpty(template))
            {
                var model = new RazorTemplateModel(this.Site, this.Page);
                this.Page.Content = this.service.Parse(template, model, null, null);
            }
        }
    }
}
