using System.IO;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RazorPress.Build
{
    /// <summary>
    /// Transforms Razor template.
    /// </summary>
    public class TransformRazorPage : RazorPageCommand
    {
        /// <summary>
        /// Transforms <see cref="Page.Content"/> using Razor templating engine.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            string template = this.Page.Content;
            if (!string.IsNullOrEmpty(template))
            {
                this.Page.Content = this.Transform(template);
            }
        }
    }
}
