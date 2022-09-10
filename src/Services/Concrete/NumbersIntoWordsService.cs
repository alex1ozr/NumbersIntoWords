using AErmilov.NumbersIntoWords.Services.Abstract;
using AErmilov.NumbersIntoWords.Services.Exceptions;
using AErmilov.NumbersIntoWords.Services.Extensions;
using System.Text;

namespace AErmilov.NumbersIntoWords.Services.Concrete;

internal sealed class NumbersIntoWordsService : INumbersIntoWordsService
{
    private static readonly string[] underTwentyWords = new[] {
        "zero", "one", "two", "three", "four",
        "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "fourteen", "fifteen",
        "sixteen", "seventeen", "eighteen", "nineteen"};

    private static readonly string[] tensWords = new[] {
        string.Empty, string.Empty,
        "twenty", "thirty", "forty", "fifty",
        "sixty",  "seventy", "eighty","ninety" };

    private static readonly string[] scaleWords = new[] { string.Empty, "thousand", "million" };

    private const string Hundred = "hundred";
    private const string SingleDollar = "dollar";
    private const string PluralDollars = "dollars";
    private const string SingleCent = "cent";
    private const string PluralCents = "cents";

    public string NumberIntoWords(decimal num)
    {
        ValidateNumber(num);

        var result = new StringBuilder();

        var intPart = (long)num;
        result.Append(NumberIntoWordsInternal(intPart))
            .Append(' ')
            .Append(intPart == 1 ? SingleDollar : PluralDollars);

        var decimalPart = (long)(num * 100) % 100;
        if (decimalPart > 0)
        {
            result.Append(" and ")
                .Append(NumberIntoWordsInternal(decimalPart))
                .Append(' ')
                .Append(decimalPart == 1 ? SingleCent : PluralCents);
        }

        return result.ToString();
    }

    private static string NumberIntoWordsInternal(long number)
    {
        if (number < 20)
        {
            return underTwentyWords[number];
        }
        else if (number < 100)
        {
            var tens = number / 10;
            var tenRemainder = number % 10;

            return tenRemainder > 0
                ? $"{tensWords[tens]}-{NumberIntoWordsInternal(tenRemainder)}"
                : $"{tensWords[tens]}";
        }
        else if (number < 1000)
        {
            var hundreds = number / 100;
            var hundredRemainder = number % 100;

            return hundredRemainder > 0
                ? $"{underTwentyWords[hundreds]} {Hundred} {NumberIntoWordsInternal(hundredRemainder)}"
                : $"{underTwentyWords[hundreds]} {Hundred}";
        }
        else
        {
            var powerOfThousand = (int)Math.Log10(number) / 3;
            var tenInNearestMaxPower = (int)Math.Pow(10, powerOfThousand * 3);

            var scaledNumber = number / tenInNearestMaxPower;
            var remainder = number % tenInNearestMaxPower;

            return remainder > 0
                ? $"{NumberIntoWordsInternal(scaledNumber)} {scaleWords[powerOfThousand]} {NumberIntoWordsInternal(remainder)}"
                : $"{NumberIntoWordsInternal(scaledNumber)} {scaleWords[powerOfThousand]}";
        }
    }

    private void ValidateNumber(decimal num)
    {
        const int MinDollarsValue = 0;
        const decimal MaxDollarsValue = 999_999_999.99M;

        if (num < MinDollarsValue
            || num > MaxDollarsValue)
        {
            throw new OutOfRangeNumberException();
        }

        if (num.HasMoreThan2DecimalPlaces())
        {
            throw new MoreThanTwoDecimalPlacesException();
        }
    }
}
