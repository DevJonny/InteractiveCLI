namespace InteractiveCLI.Options;

[Verb("interactive", true)]
public class InteractiveOptions : IOptions
{
    public LogLevel LogLevel { get; set; }
}