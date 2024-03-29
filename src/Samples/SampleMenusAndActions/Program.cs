﻿using InteractiveCLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleMenusAndActions.Menus;
using SampleMenusAndActions.Options;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
    
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateBootstrapLogger();

var host = 
    Host.CreateDefaultBuilder()
        .AddInteractiveCli<InteractiveOptions>(configuration)
        .Build();

host.UseInteractiveCli<InteractiveOptions, TopLevelMenu>(options => 
    new TopLevelMenu(options, host.Services.GetService<ILogger>()!), args);