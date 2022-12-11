using CommandLine;
using Paramore.Brighter;

namespace SampleMenusAndActions.Options;

[Verb("interactive", true)]
public class InteractiveOptions : Command, IOptions
{
    public LogLevel LogLevel { get; set; }

    public InteractiveOptions() : base(Guid.NewGuid())
    {
    }
}