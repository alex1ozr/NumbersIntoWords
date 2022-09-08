using AErmilov.NumbersIntoWords.Services.Exceptions;

namespace AErmilov.NumbersIntoWords.Services.Abstract;

/// <summary>
/// Service for converting the number into words
/// </summary>
public interface INumbersIntoWordsService
{
    /// <summary>
    /// Convert into words
    /// </summary>
    /// <exception cref="OutOfRangeNumberException">The number is out of range</exception>
    string NumberIntoWords(decimal num);
}
