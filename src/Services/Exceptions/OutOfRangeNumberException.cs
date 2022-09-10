namespace AErmilov.NumbersIntoWords.Services.Exceptions;

/// <summary>
/// Out of range number exception
/// </summary>
public sealed class OutOfRangeNumberException : ServiceException
{
    /// <inheritdoc />
	public OutOfRangeNumberException()
		: base("The number should be between 0 and 999 999 999.99")
	{

	}

    /// <inheritdoc />
    public override string ErrorCode => "wrong_number";

    /// <inheritdoc />
    public override string ShortDescription => "The number is out of range";
}
