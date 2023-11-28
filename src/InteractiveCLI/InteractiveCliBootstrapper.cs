using CommandLine;
using InteractiveCLI.Menus;
using InteractiveCLI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
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

    public static IHostBuilder AddInteractiveCLI<TOptions>(this IHostBuilder hostBuilder, IConfiguration configuration, Action<IServiceCollection> configureServices)
        where TOptions : class, IOptions
    {
        hostBuilder.AddInteractiveCli<TOptions>(configuration, configureServices);

        return hostBuilder;
    }
    
    public static IHostBuilder AddInteractiveCli<TOptions>(
        this IHostBuilder hostBuilder,
        IConfiguration configuration,
        Action<IServiceCollection> configureServices) where TOptions : class, IOptions
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateBootstrapLogger();

        hostBuilder
            .ConfigureServices((_, services) =>
            {
                configureServices(services);
                services
                    .AddSingleton<TOptions>(_ => OptionsFactory<TOptions>.Get())
                    .AddSingleton(Log.Logger)
                    .RegisterActions();
            })
            .UseSerilog();

        return hostBuilder;
    }
    
    public static IHost UseInteractiveCli<TOptions, TMenu>(
        this IHost host, 
        Func<TOptions, TMenu> buildFirstMenu, 
        params string[] args) 
            where TMenu : Menu 
            where TOptions : class, IOptions
    {
        try
        {
            Parser.Default.ParseArguments<TOptions>(args)
                .WithParsed(options =>
                {
                    OptionsFactory<TOptions>.Set(options);
                    buildFirstMenu(options).DoAsync().Wait();
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