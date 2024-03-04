using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System.Diagnostics;

namespace MaaDaemon {
    internal class Program {
        public static void Main(string[] args) {
            // Changed to return the IHost
            // builder before running it.
            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler => {
                // Easy peasy 👇
                scheduler
                    .Schedule<MaaChecker>()
                    .EveryFiveMinutes();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {
                    services.AddScheduler();
                    // Add this 👇
                    services.AddTransient<MaaChecker>();
                }).ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options => {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "[yyyy/MM/dd HH:mm:ss] ";
                    });
                });
    }
}

