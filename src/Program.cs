using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;

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


        void ConfigureLogging(IOptions options)
        {
            var logEventLevel = options.LogLevel switch
            {
                LogLevel.Verbose => LogEventLevel.Debug,
                LogLevel.Normal => LogEventLevel.Information,
                LogLevel.Quiet => LogEventLevel.Error,
                LogLevel.Silent => LogEventLevel.Fatal
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logEventLevel)
                .WriteTo.Console(logEventLevel)
                .CreateLogger();
        }

            
        }

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
