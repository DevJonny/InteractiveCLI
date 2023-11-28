using CommandLine;
using InteractiveCLI.Options;

namespace SampleMenusAndActions.Options;

[Verb("interactive", true)]
public class InteractiveOptions : IOptions
{
    [Option('n', "name", Required = true, HelpText = "Enter your name")]
    public string Name { get; init; } = string.Empty;
}