using System;
using System.ComponentModel.Composition;
using System.Linq;
using Xunit;

namespace RazorPress.Build
{
    public class CommandAttributeTest
    {
        public class Class : CommandAttributeTest
        {
            [Fact]
            public void IsPublicForUseWhenExtendingRazorPress()
            {
                Assert.True(typeof(CommandAttribute).IsPublic);
            }

            [Fact]
            public void IsSealedToImproveRuntimePerformance()
            {
                Assert.True(typeof(CommandAttribute).IsSealed);
            }

            [Fact]
            public void InheritsFromExportAttributeToProvideExportMetadata()
            {
                Assert.True(typeof(ExportAttribute).IsAssignableFrom(typeof(CommandAttribute)));
            }

            [Fact]
            public void ImplementsICommandMetadataToEnsureItsPropertiesMatchMetadataDefinitions()
            {
                Assert.True(typeof(ICommandMetadata).IsAssignableFrom(typeof(CommandAttribute)));
            }

            [Fact]
            public void IsMarkedWithAttributeUsageAttributeToAllowApplyingItToTypesOnly()
            {
                var attributeUsage = typeof(CommandAttribute).GetCustomAttributes(false).OfType<AttributeUsageAttribute>().Single();
                Assert.Equal(AttributeTargets.Class, attributeUsage.ValidOn);
            }

            [Fact]
            public void IsMarkedWithMetadataAttrbuteForMefToRecognizeItAsCustomExportAttribute()
            {
                Assert.Equal(1, typeof(CommandAttribute).GetCustomAttributes(false).OfType<MetadataAttributeAttribute>().Count());
            }
        }

        public class Constructor : CommandAttributeTest
        {
            [Fact]
            public void InitializesContractTypeWithCommand()
            {
                var attribute = new CommandAttribute();
                Assert.Equal(typeof(Command), attribute.ContractType);
            }

            [Fact]
            public void InitializesDependsOnPropertyWithEmptyArrayToPreventUsageErrors()
            {
                var attribute = new CommandAttribute();
                Type[] dependsOn = attribute.DependsOn;
                Assert.Empty(dependsOn);
            }
        }

        public class DependsOn : CommandAttributeTest
        {
            [Fact]
            public void CanBeSetWhenAttributeIsAppliedToCommand()
            {
                var attribute = new CommandAttribute();
                var dependsOn = new[] { typeof(TransformMarkdownPages) };
                attribute.DependsOn = dependsOn;
                Assert.Equal(dependsOn, attribute.DependsOn);
            }

            [Fact]
            public void ThrowsArgumentExceptionToPreventUsageErrors()
            {
                var attribute = new CommandAttribute();
                Assert.Throws<ArgumentNullException>(() => attribute.DependsOn = null);
                Assert.Throws<ArgumentException>(() => attribute.DependsOn = new[] { typeof(object) });
                Assert.Throws<ArgumentException>(() => attribute.DependsOn = new Type[] { null });
            }
        }
    }
}
