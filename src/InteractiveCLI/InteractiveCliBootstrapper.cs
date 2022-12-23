using CommandLine;
using InteractiveCLI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Spectre.Console;

namespace InteractiveCLI;

public static class InteractiveCliBootstrapper
{
    public static IHostBuilder AddInteractiveCli<TOptions>(this IHostBuilder hostBuilder, IConfiguration configuration) 
        where TOptions : class, IOptions
    {
        hostBuilder.AddInteractiveCli<TOptions>(configuration, _ => { });

        return hostBuilder;
    }
    
    public static IHostBuilder AddInteractiveCli<TOptions>(
        this IHostBuilder hostBuilder,
        IConfiguration configuration,
        Action<IServiceCollection> configureServices) where TOptions : class, IOptions
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
                    .AddSingleton<TOptions>(_ => OptionsFactory<TOptions>.Get())
                    .AddBrighter().AutoFromAssemblies();
            })
            .UseSerilog();

        return hostBuilder;
    }
    
    public static IHost UseInteractiveCli<TOptions, TMenu>(
        this IHost host, 
        Func<TOptions, TMenu> buildFirstMenu, 
        params string[] args) 
            where TMenu : Command 
            where TOptions : class, IOptions
    {
        try
        {
            Parser.Default.ParseArguments<TOptions>(args)
                .WithParsed(options =>
                {
                    OptionsFactory<TOptions>.Set(options);
                    var commandProcessor = host.Services.GetService<IAmACommandProcessor>();
                    commandProcessor?.SendAsync(buildFirstMenu(options)).Wait();
                });
        }
        catch (AggregateException e)
        {
            if (e.InnerException is StopApplicationAction)
                AnsiConsole.WriteLine("Goodbye!");
            else
                throw e.InnerException!;
        }
        catch (Exception e)
        {
            Log.Fatal(e, "A Fatal error has occured");
        }

        return host;
    }
}