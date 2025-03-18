using CommandLine;
using InteractiveCLI.Menus;
using InteractiveCLI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console;

namespace InteractiveCLI;

public static class InteractiveCliBootstrapper
{
    public static IServiceProvider ServiceProvider;

    public static IHostBuilder AddInteractiveCli(this IHostBuilder hostBuilder)
    {
        hostBuilder.AddInteractiveCli<EmptyOptions>(new ConfigurationBuilder().Build());

        return hostBuilder;
    }

    public static IHostBuilder AddInteractiveCli(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        hostBuilder.AddInteractiveCli<EmptyOptions>(configuration);

        return hostBuilder;
    }

    public static IHostBuilder AddInteractiveCli(
        this IHostBuilder hostBuilder,
        IConfiguration configuration,
        Action<IServiceCollection> configureServices)
    {
        hostBuilder.AddInteractiveCli<EmptyOptions>(configuration, configureServices);

        return hostBuilder;
    }
    
    public static IHostBuilder AddInteractiveCli<TOptions>(this IHostBuilder hostBuilder, IConfiguration configuration) 
        where TOptions : class, IOptions
    {
        hostBuilder.AddInteractiveCli<TOptions>(configuration, _ => { });

        return hostBuilder;
    }

    public static IHostBuilder AddInteractiveCLI<TOptions>(
        this IHostBuilder hostBuilder, 
        IConfiguration configuration, 
        Action<IServiceCollection> configureServices)
        where TOptions : class, IOptions
    {
        hostBuilder.AddInteractiveCli<TOptions>(configuration, configureServices);

        return hostBuilder;
    }
    
    public static IHostBuilder AddInteractiveCli<TOptions>(
        this IHostBuilder hostBuilder,
        IConfiguration? configuration,
        Action<IServiceCollection> configureServices) where TOptions : class, IOptions
    {
        LoggerConfiguration loggerConfiguration = new();

        if (configuration is not null)
            loggerConfiguration = loggerConfiguration.ReadFrom.Configuration(configuration);
        
        Log.Logger = loggerConfiguration.CreateBootstrapLogger();

        hostBuilder
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<TOptions>(_ => OptionsFactory<TOptions>.Get());
                configureServices(services);
                services                    
                    .AddSingleton(Log.Logger)
                    .RegisterActions();

                ServiceProvider = services.BuildServiceProvider();
            })
            .UseSerilog();

        return hostBuilder;
    }

    public static IHost UseInteractiveCli<TMenu>(
        this IHost host,
        Func<EmptyOptions, TMenu> buildFirstMenu,
        params string[] args)
        where TMenu : Menu
        => host.UseInteractiveCli<EmptyOptions, TMenu>(buildFirstMenu, args);
    
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
