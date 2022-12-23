namespace InteractiveCLI.Options;

public class OptionsFactory<TOptions> where TOptions : class, IOptions
{
    private static TOptions? _instance;
    public static TOptions Get() => _instance ?? throw new ArgumentNullException();

    public static void Set(TOptions options) => _instance = options;
}