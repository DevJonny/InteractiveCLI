namespace InteractiveCLI.Options;

public interface IOptions
{
    [Option('v', "verbosity", Default = LogLevel.Normal, HelpText = "Verbosity of the logging")]
    public LogLevel LogLevel { get; set; }
}