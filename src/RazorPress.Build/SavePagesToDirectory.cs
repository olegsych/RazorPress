using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace RazorPress.Build
{
    /// <summary>
    /// Saves <see cref="Site.Pages"/> to the specified <see cref="TargetDirectory"/>.
    /// </summary>
    [Export]
    public class SavePagesToDirectory : PageCommand
    {
        /// <summary>
        /// Gets or sets the target directory where the <see cref="Site.Pages"/> will be saved.
        /// </summary>
        [Import("Target")]
        public DirectoryInfo TargetDirectory { get; set; }

        /// <summary>
        /// Verifies that <see cref="TargetDirectory"/> property is initialized in addition to invoking the inherited method.
        /// </summary>
        public override void Execute()
        {
            if (this.TargetDirectory == null)
            {
                throw new InvalidOperationException("Target directory must be initialized.");
            }

            base.Execute();
        }

        /// <summary>
        /// Saves the given <see cref="Page"/> to the output <see cref="TargetDirectory"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This method is called only by the base class.")]
        protected override void Execute(Page page)
        {
            string relativeFilePath = page.Url.TrimStart('/').Replace('/', '\\');
            string absoluteFilePath = Path.Combine(this.TargetDirectory.FullName, relativeFilePath);
            Directory.CreateDirectory(Path.GetDirectoryName(absoluteFilePath));
            File.WriteAllText(absoluteFilePath, page.Content);
        }
    }
}
