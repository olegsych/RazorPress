using System;
using System.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace RazorPress
{
    /// <summary>
    /// Encapsulates configuration of RazorPress web site generator.
    /// </summary>
    [Shared, Export]
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Justification = "System.Configuration and RazorPress.Configuration will not be used in the same context.")]
    public class Configuration
    {
        private DirectoryInfo sourceDirectory;
        private DirectoryInfo targetDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            this.sourceDirectory = new DirectoryInfo(".");
            this.targetDirectory = new DirectoryInfo(".\\_site");
        }

        /// <summary>
        /// Gets or sets the directory where source files of the web site are located.
        /// </summary>
        [Export("Source")]
        public DirectoryInfo SourceDirectory 
        {
            get { return this.sourceDirectory; }
            set { Property.Set(ref this.sourceDirectory, value); }
        }
        
        /// <summary>
        /// Gets or sets the directory where web site files will be generated.
        /// </summary>
        [Export("Target")]
        public DirectoryInfo TargetDirectory 
        {
            get { return this.targetDirectory; }
            set { Property.Set(ref this.targetDirectory, value); }
        }

        /// <summary>
        /// Sets properties of the <paramref name="target"/> instance.
        /// </summary>
        public void Update(Configuration target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.SourceDirectory = this.SourceDirectory;
            target.TargetDirectory = this.TargetDirectory;
        }
    }
}
