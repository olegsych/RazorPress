using System.Diagnostics.CodeAnalysis;
using System.IO;
using CLAP;
using CLAP.Validation;
using RazorPress.Build;

namespace RazorPress.Console
{
    internal class Program
    {
        internal static int Main(string[] args)
        {
            return Parser.RunConsole<Program>(args);
        }

        [Verb(IsDefault = true)]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This method is called by CLAP.Parser")] 
        private static void Render(
            [Required, FileExists]            
            string inputFile, 
            string outputFile)
        {
            var page = new Page('/' + Path.GetFileName(inputFile)) { Content = File.ReadAllText(inputFile) };

            // TODO: Change Program to support site directory.
            using (var razor = new TransformRazorPages())
            {
                razor.Site = new Site { Pages = { page } };
                razor.Execute();
            }

            File.WriteAllText(outputFile, page.Content);
        }
    }
}
