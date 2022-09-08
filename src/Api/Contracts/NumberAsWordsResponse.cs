using JetBrains.Annotations;

namespace AErmilov.NumbersIntoWords.Api.Contracts
{
    /// <summary>
    /// Number into words conversion result
    /// </summary>
    [PublicAPI]
    public sealed class NumberAsWordsResponse
    {
        /// <summary>
        /// The number
        /// </summary>
        public decimal Number { get; set; }

        /// <summary>
        /// Converted words
        /// </summary>
        public string? Words { get; set; }
    }
}
