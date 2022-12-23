using CommandLine;
using InteractiveCLI.Options;
using Paramore.Brighter;

namespace SampleMenusAndActions.Options;

[Verb("interactive", true)]
public class InteractiveOptions : Command, IOptions
{
    [Option('n', "name", Required = true, HelpText = "Enter your name")]
    public string Name { get; init; } = string.Empty;

    public InteractiveOptions() : base(Guid.NewGuid())
    {
    }
}