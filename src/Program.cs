using System;
using System.Linq;
using System.Threading.Tasks;
using InteractiveCLI.Actions;
using InteractiveCLI.Commands;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace InteractiveCLI
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;
        
        public static void Main(string[] args)
        {
            try
            {
                var app = new CommandApp<InteractiveCommand>();
                app.Configure(config =>
                {
                    config
                        .AddCommand<InteractiveCommand>("interactive")
                        .WithDescription("Launches the app");
#if DEBUG
                    config.PropagateExceptions();
                    config.ValidateExamples();
                    
                    var services = new ServiceCollection();
            
                    foreach (var action in typeof(ActionBase).Assembly.GetTypes().Where(t => t.BaseType == typeof(ActionBase)))
                        services.AddSingleton(action);

                    _serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
                    {
                        ValidateScopes = true
                    });
#endif
                });
            }
            catch (Exception e)
            {

            }
            finally
            {
                DisposeServices();
            }

            
        }
        
        // void ConfigureLogging(IGeneralOptions options)
        // {
        //     var logEventLevel = options.DebugLogging
        //         ? LogEventLevel.Debug
        //         : options.ErrorLogging
        //             ? LogEventLevel.Error
        //             : LogEventLevel.Information;
        //     
        //     if (!options.SilentOnError)
        //     {
        //         Log.Logger = new LoggerConfiguration()
        //             .MinimumLevel.Is(logEventLevel)
        //             .WriteTo.Console(logEventLevel)
        //             .WriteTo.File(Path.Join(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".huddle"), "log.txt"), LogEventLevel.Error, rollingInterval: RollingInterval.Day)
        //             .CreateLogger();
        //     }
        // }

        private static void DisposeServices()
        {
            switch (_serviceProvider)
            {
                case null:
                    return;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }
    }
}
