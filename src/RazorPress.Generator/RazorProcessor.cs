using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Generator
{
    internal class RazorProcessor : IPageProcessor
    {
        private readonly TemplateServiceConfiguration configuration;
        private readonly TemplateService service;

        public RazorProcessor()
        {
            this.configuration = new TemplateServiceConfiguration();
            this.configuration.BaseTemplateType = typeof(Template);
            this.service = new TemplateService(configuration);
        }

        public void Process(Page page)
        {
            string template = page.Content;

            if (!string.IsNullOrEmpty(template))
            {
                var model = new Model(page);
                page.Content = this.service.Parse(template, model, null, null);
            }
        }
    }
}
