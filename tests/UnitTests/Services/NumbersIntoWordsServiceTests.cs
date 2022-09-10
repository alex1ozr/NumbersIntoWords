using AErmilov.NumbersIntoWords.Services.Abstract;
using AErmilov.NumbersIntoWords.Services.Concrete;
using AErmilov.NumbersIntoWords.Services.Exceptions;
using FluentAssertions;
using Xunit;

namespace AErmilov.NumbersIntoWords.UnitTests.Services;
public sealed class NumbersIntoWordsServiceTests
{
    private readonly INumbersIntoWordsService sut = new NumbersIntoWordsService();

    [Theory(DisplayName = "Execute NumberIntoWords. Positive test.")]
    [MemberData(nameof(PositiveTestData))]
    public void NumberIntoWords_ShouldReturn(decimal number, string expectedResult)
    {
        var result = sut.NumberIntoWords(number);

        result.Should().Be(expectedResult);
    }

    [Theory(DisplayName = "Execute NumberIntoWords with out of range input parameter.")]
    [InlineData(-0.01)]
    [InlineData(999_999_999.991)]
    public void NumberIntoWords_WrongNumber_ShouldThrow(decimal number)
    {
        Action a = () => sut.NumberIntoWords(number);
        a.Should().Throw<OutOfRangeNumberException>();
    }

    public static IEnumerable<object[]> PositiveTestData()
    {
        yield return new object[] { 0M, "zero dollars" };
        yield return new object[] { 1M, "one dollar" };
        yield return new object[] { 0.01M, "zero dollars and one cent" };
        yield return new object[] { 1.01M, "one dollar and one cent" };
        yield return new object[] { 25.1M, "twenty-five dollars and ten cents" };
        yield return new object[] { 501M, "five hundred one dollars" };
        yield return new object[] { 45_100M, "forty-five thousand one hundred dollars" };
        yield return new object[] { 123_456_789.87M,
            "one hundred twenty-three million four hundred "
            + "fifty-six thousand seven hundred eighty-nine "
            + "dollars and eighty-seven cents" };
        yield return new object[] { 999_999_999.99M,
            "nine hundred ninety-nine million nine hundred "
            + "ninety-nine thousand nine hundred ninety-nine "
            + "dollars and ninety-nine cents" };
    }
}
