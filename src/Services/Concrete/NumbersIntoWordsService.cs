using AErmilov.NumbersIntoWords.Services.Abstract;
using AErmilov.NumbersIntoWords.Services.Exceptions;
using AErmilov.NumbersIntoWords.Services.Extensions;

namespace AErmilov.NumbersIntoWords.Services.Concrete;

internal sealed class NumbersIntoWordsService : INumbersIntoWordsService
{
    private const int MinDollarsValue = 0;
    private const decimal MaxDollarsValue = 999_999_999.99M;
    public string NumberIntoWords(decimal num)
    {
        if (num < MinDollarsValue 
            || num > MaxDollarsValue 
            || num.HasMoreThan2DecimalPlaces())
        {
            throw new OutOfRangeNumberException();
        }

        // TODO Put algorithm here

        return num.ToString();
    }
}
