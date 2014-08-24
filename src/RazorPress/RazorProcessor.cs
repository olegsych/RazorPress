using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress
{
    internal class RazorProcessor
    {
        private readonly TemplateServiceConfiguration configuration;
        private readonly TemplateService service;

        public RazorProcessor()
        {
            this.configuration = new TemplateServiceConfiguration();
            this.configuration.BaseTemplateType = typeof(Template);
            this.service = new TemplateService(configuration);
        }

        public string Render(string template, Model model)
        {
            if (string.IsNullOrEmpty(template))
            {
                return string.Empty;
            }

            return this.service.Parse(template, model, null, null);
        }
    }
}
