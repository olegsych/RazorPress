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

        [Verb(IsDefault=true)]
        private static void Render(
            [Required, FileExists]            
            string inputFile, 
            string outputFile)
        {
            var page = new Page('/' + Path.GetFileName(inputFile)) { Content = File.ReadAllText(inputFile) };

            var razor = new TransformRazorPages();
            // TODO: Change Program to support site directory.
            razor.Site = new Site { Pages = { page } };
            razor.Execute();

            File.WriteAllText(outputFile, page.Content);
        }
    }
}
