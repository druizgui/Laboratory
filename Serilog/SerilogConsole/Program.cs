
namespace SerilogConsole
{
    using System;
    using System.Threading;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Logger.Information("Hello word with Serilog console app");

            for (var i = 0; i < 10; i++)
            {
                Log.Logger.Verbose("Verbose " + i);
                Log.Logger.Debug("Debug " + i);
                Log.Logger.Information("Information " + i);
                Log.Logger.Warning("Warning " + i);
                Log.Logger.Error("Error " + i);
                Log.Logger.Fatal("Fatal " + i);
                Thread.Sleep(200);
            }
        }
    }
}
