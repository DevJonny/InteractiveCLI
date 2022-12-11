using InteractiveCLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SampleMenusAndActions.Menus;
using SampleMenusAndActions.Options;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    
var host = 
    Host.CreateDefaultBuilder()
        .AddInteractiveCli(configuration)
        .Build();

host.UseInteractiveCli<InteractiveOptions, TopLevelMenu>(_ => new TopLevelMenu(), args);