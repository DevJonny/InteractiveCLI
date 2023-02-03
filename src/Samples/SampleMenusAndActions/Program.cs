
using InteractiveCLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SampleMenusAndActions.Menus;
using SampleMenusAndActions.Options;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var host = 
    Host.CreateDefaultBuilder()
        .AddInteractiveCli<InteractiveOptions>(configuration)
        .Build();

host.UseInteractiveCli<InteractiveOptions, TopLevelMenu>(_ => new TopLevelMenu(), args);