using FaimDemoRunner;
using Serilog;


var builder = new ConfigurationBuilder();

// Specifying the configuration for serilog
Log.Logger = new LoggerConfiguration() // initiate the logger configuration
                .Enrich.FromLogContext() //Adds more information to our logs from built in Serilog 
                .WriteTo.Console() // decide where the logs are going to be shown
                .WriteTo.File("DemoRunner.serilog.txt")
                .CreateLogger(); //initialise the logger

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>()
        .AddSingleton(Log.Logger);
    })
    .UseSerilog()
    .Build();

Log.Logger.Information("Application Starting - hello world");

await host.RunAsync();
