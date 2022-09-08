using System.ComponentModel.DataAnnotations;

namespace AErmilov.NumbersIntoWords.Api.Client.Options;

/// <summary>
/// NumbersIntoWords client options
/// </summary>
public sealed class NumbersIntoWordsClientOptions
{
    public static string OptionsKey = "NumbersIntoWords";

    private static string UrlUnsetError = $"Configuration value '{OptionsKey}.{nameof(ServerUrl)}' is not set";

    /// <summary>
    /// NumbersIntoWords.Api uri
    /// </summary>
    public Uri Url() => ServerUrl ?? throw new ArgumentNullException(nameof(ServerUrl), UrlUnsetError);

    /// <summary>
    /// NumbersIntoWords.Api uri from Options
    /// </summary>
    [Required]
    private Uri? ServerUrl { get; set; }
}
