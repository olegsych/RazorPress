using System;

namespace RazorPress.Build
{
    /// <summary>
    /// Represents metadata that determines how a <see cref="Command"/> is executed.
    /// </summary>
    public interface ICommandMetadata
    {
        /// <summary>
        /// Gets an array of <see cref="Command"/> types that should be executed before the command described by this metadata.
        /// </summary>
        Type[] DependsOn { get; }
    }
}
