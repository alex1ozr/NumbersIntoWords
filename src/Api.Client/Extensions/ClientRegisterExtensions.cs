using AErmilov.NumbersIntoWords.Api.Client.Implementations;
using AErmilov.NumbersIntoWords.Api.Client.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AErmilov.NumbersIntoWords.Api.Client.Extensions;

/// <summary>
/// Extensions for NumbersIntoWordsClient registration
/// </summary>
public static class ClientRegisterExtensions
{
    /// <summary>
    /// Add http clients into ServiceCollection
    /// </summary>
    public static IServiceCollection AddNumbersIntoWordsHttpClient(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddClientOptions();
        services.AddHttpClient<INumbersIntoWordsClient, NumbersIntoWordsClient>();

        return services;
    }

    private static IServiceCollection AddClientOptions(this IServiceCollection services)
    {
        services.AddOptions();

        services.AddConfiguredOptions<NumbersIntoWordsClientOptions>(NumbersIntoWordsClientOptions.OptionsKey);

        return services;
    }

    // Workaround for Nullable Reference Types and IOptions. More info here: https://github.com/dotnet/runtime/issues/52905
    private static void AddConfiguredOptions<TOptions>(this IServiceCollection services, string sectionKey)
        where TOptions : class, new()
    {
        services.TryAddSingleton<IConfigureOptions<TOptions>>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionKey);
            return new NonPublicConfigureOptions<TOptions>(section);
        });

        var validateOptions = new DataAnnotationValidateOptions<TOptions>(Microsoft.Extensions.Options.Options.DefaultName);
        services.TryAddSingleton<IValidateOptions<TOptions>>(validateOptions);

        services.TryAddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<TOptions>>();
            return options.Value;
        });
    }

    private class NonPublicConfigureOptions<TOptions> : IConfigureOptions<TOptions>
        where TOptions : class
    {
        private static readonly Action<BinderOptions> withNonPublicProperties = bind => bind.BindNonPublicProperties = true;
        private readonly IConfigurationSection section;

        public NonPublicConfigureOptions(IConfigurationSection section)
        {
            this.section = section;
        }

        public void Configure(TOptions options)
        {
            section.Bind(options, withNonPublicProperties);
        }
    }
}