using CommandLine;
using InteractiveCLI.Menus;
using InteractiveCLI.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services => services.AddBrighter().AutoFromAssemblies())
    .UseSerilog()
    .Build();

try
{
    Parser.Default.ParseArguments<InteractiveOptions>(args)
        .WithParsed(_ =>
        {
            var commandProcessor = host.Services.GetService<IAmACommandProcessor>();
            commandProcessor?.SendAsync(new TopLevelMenu()).Wait();
        });
}
catch (Exception e)
{
    Log.Fatal(e, "A Fatal error has occured");
}