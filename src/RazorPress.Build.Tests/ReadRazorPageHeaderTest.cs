﻿using System;
using System.IO;
using Xunit;

namespace RazorPress.Build
{
    public class ReadRazorPageHeaderTest
    {
        [Fact]
        public void ClassIsPublicForDocumentationAndExtensibility()
        {
            Assert.True(typeof(ReadRazorPageHeader).IsPublic);
        }

        [Fact]
        public void ClassInheritsFromRazorPageCommandForCodeReuseAndPolimorphism()
        {
            Assert.True(typeof(RazorPageCommand).IsAssignableFrom(typeof(ReadRazorPageHeader)));
        }

        [Fact]
        public void ExecuteRendersHeaderSectionOfRazorTemplateToInitializePageProperties()
        {
            var command = new ReadRazorPageHeader();
            command.Site = new Site(new DirectoryInfo(Path.GetRandomFileName()));
            command.Page = new Page(new FileInfo(Path.GetRandomFileName()))
            {
                Content = @"
                @{
                    this.Page.Title = ""ExpectedValue"";
                }"
            };

            command.Execute();

            Assert.Equal("ExpectedValue", command.Page.Title);
        }

        [Fact]
        public void ExecuteOverridesMethodInheritedFromBaseClassForPolymorphism()
        {
            Assert.Equal(typeof(Command).GetMethod("Execute"), typeof(ReadRazorPageHeader).GetMethod("Execute").GetBaseDefinition());
        }

        [Fact]
        public void ExecuteCallsBaseMethodForConsistentErrorHandling()
        {
            var command = new ReadRazorPageHeader();
            Assert.Throws<InvalidOperationException>(() => command.Execute());
        }
    }
}