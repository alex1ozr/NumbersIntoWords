using AErmilov.NumbersIntoWords.Services.Abstract;
using AErmilov.NumbersIntoWords.Services.Concrete;
using AErmilov.NumbersIntoWords.Services.Exceptions;
using FluentAssertions;
using Xunit;

namespace AErmilov.NumbersIntoWords.UnitTests.Services;
public sealed class NumbersIntoWordsServiceTests
{
    private readonly INumbersIntoWordsService sut;

	public NumbersIntoWordsServiceTests()
	{
        sut = new NumbersIntoWordsService();
    }

    [Theory(DisplayName = "Execute NumberIntoWords with out of range input parameter.")]
    [InlineData(-0.01)]
    [InlineData(999_999_999.991)]
    public void NumberIntoWords_WrongNumber_ShouldThrow(decimal number)
    {
        Action a = () => sut.NumberIntoWords(number);
        var exception = a.Should().Throw<OutOfRangeNumberException>();
    }
}
