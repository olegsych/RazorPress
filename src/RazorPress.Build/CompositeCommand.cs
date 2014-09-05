using System;
using System.Collections;
using System.Collections.Generic;

namespace RazorPress.Build
{
    /// <summary>
    /// Serves as a base class for commands that execute other commands.
    /// </summary>
    public abstract class CompositeCommand : Command
    {
        private readonly IReadOnlyCollection<Command> dependsOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeCommand"/> class.
        /// </summary>
        /// <param name="dependsOn">A read-only collection of <see cref="Command"/> objects this instance will execute.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="dependsOn"/> argument is null.</exception>
        /// <exception cref="ArgumentException">The <paramref name="dependsOn"/> array is empty contains null elements.</exception>
        public CompositeCommand(IReadOnlyCollection<Command> dependsOn)
        {
            if (dependsOn == null)
            {
                throw new ArgumentNullException();
            }

            if (dependsOn.Count == 0)
            {
                throw new ArgumentException("One or more Command objects expected.");
            }

            foreach (Command command in dependsOn)
            {
                if (command == null)
                {
                    throw new ArgumentException("Command objects cannot be null.");
                }
            }

            this.dependsOn = dependsOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeCommand"/> class without dependencies. 
        /// Use this constructor only in test code.
        /// </summary>
        protected CompositeCommand()
        {
            this.dependsOn = new Command[0];
        }

        /// <summary>
        /// Gets a read-only collection of <see cref="Command"/> objects executed by this command.
        /// </summary>
        /// <remarks>
        /// This property mimics the DependsOnTargets attribute of MSBuild targets.
        /// http://msdn.microsoft.com/en-us/library/t50z2hka.aspx.
        /// </remarks>
        public IReadOnlyCollection<Command> DependsOn 
        {
            get { return this.dependsOn; }
        }

        /// <summary>
        /// Executes the <see cref="Command"/> instances this <see cref="CompositeCommand"/> was constructed with.
        /// </summary>
        public sealed override void Execute()
        {
            base.Execute();

            foreach (Command command in this.dependsOn)
            {
                command.Execute();
            }
        }
    }
}
