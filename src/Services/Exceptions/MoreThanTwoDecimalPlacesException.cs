namespace AErmilov.NumbersIntoWords.Services.Exceptions;

/// <summary>
/// The number contains more than 2 decimal places
/// </summary>
public sealed class MoreThanTwoDecimalPlacesException : ServiceException
{
    /// <inheritdoc />
	public MoreThanTwoDecimalPlacesException()
		: base("The number should contain not more than 2 decimal places")
	{

	}

    /// <inheritdoc />
    public override string ErrorCode => "too_many_decimal_places";

    /// <inheritdoc />
    public override string ShortDescription => "The number contains more than 2 decimal places";
}
