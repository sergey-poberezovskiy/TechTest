using System;
using System.Threading;
using TechTest.Web.Utils;

namespace TechTest.Web.Factory
{
	public class ApiClientFactory
	{
        private static Uri _apiUri;

        private static Lazy<ApiClient> _restClient = new Lazy<ApiClient>(
          () => new ApiClient(_apiUri),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            _apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static ApiClient Instance { get => _restClient.Value; }
    }
}
