using System.IO;
using CLAP;
using CLAP.Validation;
using RazorPress.Generator;

namespace RazorPress
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
            string input = File.ReadAllText(inputFile);
            var razor = new RazorProcessor();
            string output = razor.Render(input, new Model());
            File.WriteAllText(outputFile, output);
        }
    }
}
