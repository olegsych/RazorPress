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
            var page = new Page(new FileInfo(inputFile));
            page.Content = File.ReadAllText(inputFile);
            var razor = new RazorProcessor();
            razor.Process(page);
            File.WriteAllText(outputFile, page.Content);
        }
    }
}
