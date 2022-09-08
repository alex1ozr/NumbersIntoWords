using AErmilov.NumbersIntoWords.Api.Client.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AErmilov.NumbersIntoWords.Api.Client.Implementations;

internal partial class NumbersIntoWordsClient
{
    [ActivatorUtilitiesConstructor]
    public NumbersIntoWordsClient(
        HttpClient httpClient,
        IOptions<NumbersIntoWordsClientOptions> options)
        : this(options.Value.Url().ToString(), httpClient)
    {
    }
}
