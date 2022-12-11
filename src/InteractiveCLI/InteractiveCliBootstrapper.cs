using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace InteractiveCLI;

public static class InteractiveCliBootstrapper
{
    public static IHostBuilder AddInteractiveCli<T>(
        this IHostBuilder hostBuilder,
        IConfiguration configuration,
        Action<IServiceCollection> configureServices)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Override("Paramore.Brighter", LogEventLevel.Warning)
            .CreateLogger();

        hostBuilder
            .ConfigureServices((_, services) =>
            {
                configureServices(services);
                services
                    .AddBrighter().AutoFromAssemblies();
            })
            .UseSerilog();

        return hostBuilder;
    }
    
    public static IHost UseInteractiveCli<TOptions, TMenu>(
        this IHost host, 
        Func<TOptions, TMenu> buildFirstMenu, 
        params string[] args) where TMenu : Command
    {
        try
        {
            Parser.Default.ParseArguments<TOptions>(args)
                .WithParsed(options =>
                {
                    var commandProcessor = host.Services.GetService<IAmACommandProcessor>();
                    commandProcessor?.SendAsync(buildFirstMenu(options)).Wait();
                });
        }
        catch (Exception e)
        {
            Log.Fatal(e, "A Fatal error has occured");
        }

        return host;
    }
}