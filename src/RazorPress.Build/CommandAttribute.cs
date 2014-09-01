using System;
using System.ComponentModel.Composition;

namespace RazorPress.Build
{
    /// <summary>
    /// Custom export attribute for dynamic discovery of <see cref="Command"/> types through MEF.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class)]
    public sealed class CommandAttribute : ExportAttribute, ICommandMetadata
    {
        private Type[] dependsOn = new Type[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        public CommandAttribute() : base(typeof(Command))
        {
        }

        /// <summary>
        /// Gets or sets an array of <see cref="Command"/> types that should be executed before the command to which this attribute is applied.
        /// </summary>
        public Type[] DependsOn 
        {
            get 
            { 
                return this.dependsOn; 
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                foreach (Type item in value)
                {
                    if (!typeof(Command).IsAssignableFrom(item))
                    {
                        throw new ArgumentException("Expecting an array of types that inherit from Command");
                    }
                }

                this.dependsOn = value;
            }
        }
    }
}
