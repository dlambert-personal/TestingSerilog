using Serilog;

namespace FaimDemoRunner
{
    public class Worker : BackgroundService
    {
        //private readonly Serilog.ILogger<Worker> _logger;
        private readonly Serilog.ILogger _logger;

        public Worker(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            { 
                _logger.Information("Worker running at: {time}", DateTimeOffset.Now);

                //var faim = new FAIMlib.FileReader(_logger);
                // ? File.Exists(@"C:\Users\dlamb\source\repos\FAIMdemo\FAIM file.txt");
                //faim.ParseRawRecs(@"FAIM file.txt");
                //faim.ParseRawRecs(@".\..\..\..\..\..\FAIM file.txt");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}