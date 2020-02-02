using System.Threading.Tasks;

namespace TechTest.Web
{
	public partial class ApiClient
	{
		public async Task<string> CurrencyToWords(double value, bool inUpperCase)
		{
			var requestUrl = createRequestUri($"checkwriter/{value}/{inUpperCase}");
			return await getAsync<string>(requestUrl);
		}
	}
}
