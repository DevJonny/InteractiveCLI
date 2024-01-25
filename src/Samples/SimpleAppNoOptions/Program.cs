using InteractiveCLI;
using Microsoft.Extensions.Hosting;
using Serilog;
using SimpleApp.Menus;

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();

var host =
    Host.CreateDefaultBuilder()
        .AddInteractiveCli()
        .Build();
        
host.UseInteractiveCli<TopLevelMenu>(_ => new TopLevelMenu(), args);