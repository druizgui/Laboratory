using System.IO;
using CommandLine;

namespace FileExplorer
{
    public class CommandLineOptions
    {
        [Value(0, Default = ".", Required = false)]
        public string Path { get; set; }

        [Option('p', "pattern", Default = new[] { "*.*" },
            Required = false)]
        public string[] Patterns { get; set; }

        public SearchOption AllFiles { get; set; } = SearchOption.AllDirectories;
    }
}