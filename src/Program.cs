using InteractiveCLI.Commands;
using Spectre.Console.Cli;

namespace InteractiveCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandApp<InteractiveCommand>();
            app.Configure(config =>
            {
                config
                    .AddCommand<InteractiveCommand>("interactive")
                    .WithDescription("Launches the app");
#if DEBUG
                config.PropagateExceptions();
                config.ValidateExamples();
#endif
            });
        }
    }
}
