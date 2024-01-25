using System.Globalization;
using InteractiveCLI.Actions;

namespace InteractiveCLI;

public static class Extensions
{
    public static IServiceCollection RegisterActions(this IServiceCollection serviceCollection)
    {
        var appDomainAssemblies = 
            AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => 
                    !a.IsDynamic 
                    && !a.FullName!.StartsWith("Microsoft.", true, CultureInfo.InvariantCulture) 
                    && !a.FullName.StartsWith("System.", true, CultureInfo.InvariantCulture)
                    && !a.FullName.StartsWith("Serilog", true, CultureInfo.InvariantCulture)
                    && !a.FullName.StartsWith("netstandard", true, CultureInfo.InvariantCulture))
                .ToList();

        var definedActions = appDomainAssemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => 
                t.ImplementedInterfaces.Contains(typeof(IAmAnAction)) 
                && t is { IsClass: true, IsAbstract: false })
            .Distinct();
        

        foreach (var action in definedActions)
        {
            serviceCollection.AddSingleton(action.AsType());
        }

        return serviceCollection;
    }
}