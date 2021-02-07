using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace FileExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("fileExplorer.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Environment.ExitCode = Parser.Default
                .ParseArguments<CommandLineOptions>(args).MapResult(
                    RunOptionsAndReturnExitCode, HandleParseError);
        }

        private static int RunOptionsAndReturnExitCode(CommandLineOptions o)
        {
            //Log.Logger.Information("Find files ");
            Console.WriteLine(
                "Size;CreationDate;LastAccess;FileName");

            foreach (var pattern in o.Patterns)
            {
                foreach (var file in GetFiles(o.Path, pattern)
                )
                    Console.WriteLine(
                        $"{file.Length,10};{file.CreationTime.ToShortDateString()};{file.LastAccessTime.ToShortDateString()};{file.FullName}");
            }

            return 0;
        }

        private static string GetFileSize(in long length)
        {
            if (length > 1000000000000) return Math.Round((decimal) length / 1000000000000, 0) + " TB";
            if (length > 1000000000) return Math.Round((decimal) length / 1000000000, 0) + " GB";
            if (length > 1000000) return Math.Round((decimal) length / 1000000, 0) + " MB";
            if (length > 1000) return Math.Round((decimal) length / 1000, 0) + " KB";
            return length + "  B";
        }

        //in case of errors or --help or --version
        private static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            var enumerable = errs as Error[] ?? errs.ToArray();
            Console.WriteLine("errors {0}", enumerable.Count());
            if (enumerable.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }

        public static IEnumerable<FileInfo> GetFiles(string root, string searchPattern)
        {
            var pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count != 0)
            {
                var path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
                }
                catch
                {
                }

                if (next != null && next.Length != 0)
                    foreach (var file in next)
                    {
                        yield return new FileInfo(file);
                    }
                        
                try
                {
                    next = Directory.GetDirectories(path);
                    foreach (var subdir in next) pending.Push(subdir);
                }
                catch
                {
                }
            }
        }
    }
}