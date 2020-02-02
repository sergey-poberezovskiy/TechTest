using System;
using System.Collections.Generic;

namespace TechTest.Web.Services
{
	/// <summary>
	/// The service to process check writing related operations.
	/// </summary>
	public class CheckWriterService : ICheckWriterService
	{
		/// <summary>
		/// Dictionary of converting numbers into words.
		/// </summary>
		private static readonly Dictionary<int, string> _words = new Dictionary<int, string>
		{
			{0, ""},
			{1, "one"},
			{2, "two"},
			{3, "three"},
			{4, "four"},
			{5, "five"},
			{6, "six"},
			{7, "seven"},
			{8, "eight"},
			{9, "nine"},
			{10, "ten"},
			{11, "eleven"},
			{12, "twelve"},
			{13, "thirteen"},
			{14, "fourteen"},
			{15, "fifteen"},
			{16, "sixteen"},
			{17, "seventeen"},
			{18, "eighteen"},
			{19, "nineteen"},
			{20, "twenty"},
			{30, "thirty"},
			{40, "forty"},
			{50, "fifty"},
			{60, "sixty"},
			{70, "seventy"},
			{80, "eighty"},
			{90, "ninety"},
		};

		/// <summary>
		/// Supported thousand multipliers.
		/// </summary>
		private readonly static string[] _groups = new[]
		{
			"thousand",
			"million",
			"billion",
			"trillion",
			//"quadrillion",
			//"quintillion",
			//"sextillion",
			//"septillion",
			//"octillion",
			//"nonillion",
			//"decillion",...
		};

		/// <summary>
		/// Converts a currency value into words.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="negativeIndicator">The string to represent negative value.</param>
		/// <param name="inUpperCase">indicator whether to return the result in upper case.</param>
		/// <returns>Worded representation of the currency <paramref name="value"/>..</returns>
		/// <exception cref="ArgumentOutOfRangeException">The exception is thrown when the number exceeds the double precision.</exception>
		public string CurrencyToWords(double value, string negativeIndicator = "negative", bool inUpperCase = false)
		{
			double absValue = Math.Abs(value);
			if (absValue > Math.Pow(10, (_groups.Length + 1) * 3))
			{
				// rounding precision is not accurate
				throw new ArgumentOutOfRangeException(nameof(value), "The number is too big.");
			}

			long dollars = (long)Math.Floor(absValue);
			long cents = (long)Math.Round((absValue - dollars) * 100, MidpointRounding.AwayFromZero);

			// format the output
			string result = toWords(dollars) + $" dollar{getEnding(dollars)} "
						  + (cents != 0
							 ? "and " + toWords(cents) + $" cent{getEnding(cents)}"
							 : "only");

			// add negative indicator if required
			if (value < 0)
			{
				result = negativeIndicator + " " + result;
			}

			return inUpperCase
				? result.ToUpper()
				: result;
		}

		/// <summary>
		/// Returns plural or singular ending depending on the value.
		/// </summary>
		/// <param name="value">The value to return the value for.</param>
		/// <returns>The calculated ending.</returns>
		private static string getEnding(long value) => value != 1 ? "s" : "";

		/// <summary>
		/// Converts positive long value into words.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>Worded representation of the <paramref name="value"/>.</returns>
		private static string toWords(long value)
		{
			string result;
			if (value == 0)
			{
				result = "zero";
			}
			else
			{
				var list = new List<string>();
				bool addAnd = false;
				if (value >= 1000)
				{
					// process 1000 groups:
					int power = (_groups.Length + 1) * 3;
					while (power > 0)
					{
						double group = Math.Pow(10, power);
						if (value >= group)
						{
							double temp = value / group;
							if (temp >= 1)
							{
								list.Add($"{toWords((long)Math.Floor(temp))} {_groups[power / 3 - 1]}");
								value %= (long)group;
								addAnd = true;
							}
						}
						power -= 3;
					}
				}

				// add hundreds
				if (value / 100 > 0)
				{
					list.Add($"{toWords(value / 100)} hundred");
					value %= 100;
					addAnd = true;
				}

				// ensure to add and separator when required
				if (addAnd && value > 0)
				{
					list.Add("and");
				}

				// add tens
				if (value > 20)
				{
					list.Add(_words[(int)(value / 10) * 10]);
					value %= 10;
					if (value > 0)
					{
						list.Add("-");
					}
				}

				// whne the remaining value is 20 or less - just get it from the dictionary
				if (value > 0)
				{
					list.Add(_words[(int)value]);
				}

				// join the elements and remove spaces around the dashes
				result = string.Join(' ', list).Replace(" - ", "-");
			}
			return result;
		}
	}
}
