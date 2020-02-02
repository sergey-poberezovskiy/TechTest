namespace TechTest.Web.Services
{
	/// <summary>
	/// The interface for check writing related operations.
	/// </summary>
	public interface ICheckWriterService
	{
		/// <summary>
		/// Converts a currency value into words.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="negativeIndicator">The string to represent negative value.</param>
		/// <param name="inUpperCase">indicator whether to return the result in upper case.</param>
		/// <returns>Worded representation of the currency <paramref name="value"/>..</returns>
		/// <exception cref="ArgumentOutOfRangeException">The exception is thrown when the number exceeds the double precision.</exception>
		string CurrencyToWords(double value, string negativeIndicator = "negative", bool inUpperCase = false);
	}
}
