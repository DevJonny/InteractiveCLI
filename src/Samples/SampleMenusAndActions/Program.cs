using InteractiveCLI;
using InteractiveCLI.Menus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SampleMenusAndActions.Options;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    
var host = 
    Host.CreateDefaultBuilder()
        .AddInteractiveCli<InteractiveOptions>(
            configuration,
            services =>
            {
                
            })
        .Build();

host.UseInteractiveCli<InteractiveOptions, TopLevelMenu>(_ => new TopLevelMenu(), args);