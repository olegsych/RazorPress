using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RazorPress.Build
{
    public class CompositeCommandTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(CompositeCommand).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromCommandToSupportPolymorphicExecution()
        {
            Assert.True(typeof(Command).IsAssignableFrom(typeof(CompositeCommand)));
        }

        [Fact]
        public void ClassIsAbstractAndNotMeantToBeUsedDirectly()
        {
            Assert.True(typeof(CompositeCommand).IsAbstract);
        }

        [Fact]
        public void ConstructorThrowsArgumentNullExceptionWhenCollectionIsNullToPreventUsageErrors()
        {
            Command[] commands = null;
            Assert.Throws<ArgumentNullException>(() => new TestableCompositeCommand(commands));
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenCollectionIsEmptyToPreventUsageErrors()
        {
            Assert.Throws<ArgumentException>(() => new TestableCompositeCommand(new Command[0]));
        }

        [Fact]
        public void ConstructorThrowsArgumentExceptionWhenAnyCollectionElementIsNullToPreventUsageErrors()
        {
            Assert.Throws<ArgumentException>(() => new TestableCompositeCommand(new Command[1]));
        }

        [Fact]
        public void ConstructorInitializesDependsOnPropertyWithGivenCollectionOfCommands()
        {
            var commands = new Command[] { new StubCommand(), new StubCommand() };
            var composite = new TestableCompositeCommand(commands);
            Assert.Equal(commands, composite.DependsOn);
        }

        [Fact]
        public void ParameterlessConstructorInitializesDependsOnPropertyWithEmptyCollectionForUseInMocks()
        {
            var composite = new TestableCompositeCommand();
            Assert.Empty(composite.DependsOn);
        }

        [Fact]
        public void ExecuteInvokesCommandsInOrderSpecifiedByConstructor()
        {
            var executed = new List<Command>();
            var commands = new[] 
            {
                new StubCommand { OnExecute = command => executed.Add(command) },
                new StubCommand { OnExecute = command => executed.Add(command) },
            };

            var composite = new TestableCompositeCommand(commands) { Site = new Site() };
            composite.Execute();

            Assert.Equal(commands, executed);
        }

        [Fact]
        public void ExecuteOverridesInheritedMethodToReuseInheritedCommandExecutionLogic()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(CompositeCommand).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteInvokesBaseMethodToReuseInheritedCommandExecutionLogic()
        {
            var composite = new TestableCompositeCommand(new[] { new StubCommand() });
            Assert.Throws<InvalidOperationException>(() => composite.Execute());
        }

        [Fact]
        public void ExecuteIsSealedBecauseCompositeCommandsShouldNotHaveExecutionLogicOfTheirOwn()
        {
            Assert.True(typeof(CompositeCommand).GetMethod("Execute").IsFinal);
        }

        private class TestableCompositeCommand : CompositeCommand
        {
            public TestableCompositeCommand(IReadOnlyCollection<Command> dependsOn) : base(dependsOn)
            {
            }

            public TestableCompositeCommand() : base()
            {
            }
        }

        private class StubCommand : Command
        {
            public Action<StubCommand> OnExecute = command => { };

            public override void Execute()
            {
                this.OnExecute(this);
            }
        }
    }
}
