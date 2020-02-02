using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTest.Web.Models.api;

namespace TechTest.Web
{
	public partial class ApiClient
	{
		private readonly HttpClient _httpClient;

		private readonly Uri _baseEndpoint;

		public ApiClient(Uri baseEndpoint)
		{
			_baseEndpoint = baseEndpoint ?? throw new ArgumentNullException(nameof(baseEndpoint));
			_httpClient = new HttpClient();
		}
		private static JsonSerializerSettings microsoftDateFormatSettings
		{
			get => new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
		}

		private Uri createRequestUri(string relativePath, string queryString = "")
		{
			var endpoint = new Uri(_baseEndpoint, relativePath);
			var uriBuilder = new UriBuilder(endpoint)
			{
				Query = queryString
			};
			return uriBuilder.Uri;
		}

		private async Task<T> getAsync<T>(Uri requestUrl)
		{
			var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
			response.EnsureSuccessStatusCode();
			string data = await response.Content.ReadAsStringAsync();
			return typeof(T) == typeof(string) ? (T)(object)data : JsonConvert.DeserializeObject<T>(data);
		}

		/// <summary>
		/// Common method for making POST calls
		/// </summary>
		private async Task<Message<T>> postAsync<T>(Uri requestUrl, T content)
		{
			addHeaders();
			var response = await _httpClient.PostAsync(requestUrl.ToString(), createHttpContent<T>(content));
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<Message<T>>(data);
		}
		private async Task<Message<T1>> postAsync<T1, T2>(Uri requestUrl, T2 content)
		{
			addHeaders();
			var response = await _httpClient.PostAsync(requestUrl.ToString(), createHttpContent<T2>(content));
			response.EnsureSuccessStatusCode();
			var data = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<Message<T1>>(data);
		}

		private HttpContent createHttpContent<T>(T content)
		{
			var json = JsonConvert.SerializeObject(content, microsoftDateFormatSettings);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		private void addHeaders()
		{
			_httpClient.DefaultRequestHeaders.Remove("userIP");
			_httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
		}
	}
}
