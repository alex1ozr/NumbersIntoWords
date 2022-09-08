using AErmilov.NumbersIntoWords.Services.Abstract;
using AErmilov.NumbersIntoWords.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AErmilov.NumbersIntoWords.Services.Extensions;

/// <summary>
/// Services registration methods
/// </summary>
public static class ServicesRegisterExtensions
{
    public static IServiceCollection AddNumbersIntoWordsServices(this IServiceCollection services)
    {
        services.TryAddSingleton<INumbersIntoWordsService, NumbersIntoWordsService>();

        return services;
    }
}
