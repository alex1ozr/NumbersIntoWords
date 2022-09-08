namespace AErmilov.NumbersIntoWords.Services.Extensions;

internal static class DecimalExtensions
{
    public static bool HasMoreThan2DecimalPlaces(this decimal value)
    {
        decimal multipliedValue = value * 100;
        return multipliedValue != Math.Floor(multipliedValue);
    }
}
