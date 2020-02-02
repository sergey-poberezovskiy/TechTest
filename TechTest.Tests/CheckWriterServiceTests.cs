using System;
using TechTest.Web.Services;
using Xunit;

namespace TechTest.Tests
{
	public class CheckWriterServiceTests
	{
		[Theory(DisplayName = "Validate positive")]
		[InlineData(0.59, "zero dollars and fifty-nine cents")]
		[InlineData(13, "thirteen dollars only")]
		[InlineData(5000000000000.12, "five trillion dollars and twelve cents")]
		[InlineData(123.45, "one hundred and twenty-three dollars and forty-five cents")]
		public void CurrencyToWords_PositiveNumber_Succeeds(double value, string expectedResult)
		{
			// arrange
			ICheckWriterService service = createCheckWriterService();

			// act
			string result = service.CurrencyToWords(value);

			// assert
			Assert.Equal(expectedResult, result);
		}

		[Theory(DisplayName = "Validate singular")]
		[InlineData(0, "zero dollars only")]
		[InlineData(1.05, "one dollar and five cents")]
		[InlineData(1003.01, "one thousand and three dollars and one cent")]
		public void CurrencyToWords_SingularNumbers_Succeeds(double value, string expectedResult)
		{
			// arrange
			ICheckWriterService service = createCheckWriterService();

			// act
			string result = service.CurrencyToWords(value);

			// assert
			Assert.Equal(expectedResult, result);
		}

		[Theory(DisplayName = "Validate negative")]
		[InlineData(-11, false, "negative eleven dollars only")]
		[InlineData(-.99, true, "NEGATIVE ZERO DOLLARS AND NINETY-NINE CENTS")]
		public void CurrencyToWords_NegativeNumberAndUpperCase_Succeeds(double value, bool resultInUpperCase, string expectedResult)
		{
			// arrange
			ICheckWriterService service = createCheckWriterService();

			// act
			string result = service.CurrencyToWords(value, inUpperCase:resultInUpperCase);

			// assert
			Assert.Equal(expectedResult, result);
		}

		[Theory(DisplayName = "Fail very large number")]
		[InlineData(1000000000000000.5)]
		public void CurrencyToWords_VeryLargeNumber_Fails(double value)
		{
			// arrange
			ICheckWriterService service = createCheckWriterService();

			// assert
			Assert.Throws<ArgumentOutOfRangeException>(
				() => _ = service.CurrencyToWords(value));
		}

		private ICheckWriterService createCheckWriterService() => new CheckWriterService();
	}
}
