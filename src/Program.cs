using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;

namespace InteractiveCLI;

internal class Program
{
    private static IServiceProvider _serviceProvider;

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<InteractiveOptions>(args)
            .WithParsed(o =>
            {
                try
                {
                    ConfigureLogging(o);
                    ConfigureIoc(o);

                    var command = new InteractiveCommand(_serviceProvider.GetService<Runner>());
                    command.Run();
                }
                catch (Exception e)
                {
                    Log.Error(e, "Something went wrong");
                }
                finally
                {
                    DisposeServices();
                }
            });


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

        void ConfigureIoc<T>(T options) where T : IOptions
        {
            var services = new ServiceCollection();

            services.AddSingleton(typeof(T), options);
            services.AddSingleton(provider => new Runner(provider));

            foreach (var action in typeof(ActionBase).Assembly.GetTypes().Where(t => t.BaseType == typeof(ActionBase)))
                services.AddSingleton(action);

            _serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true
            });
        }

        void DisposeServices()
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