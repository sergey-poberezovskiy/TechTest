using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechTest.Web.Factory;
using TechTest.Web.Models;
using TechTest.Web.Utils;

namespace TechTest.Web.Controllers
{
	public class HomeController : Controller
	{
		public HomeController(IOptions<MySettingsModel> options)
		{
			if (ApplicationSettings.WebApiUrl == null)
			{
				ApplicationSettings.WebApiUrl = options.Value.WebApiBaseUrl ?? MyHttpContext.AppBaseUrl + "/api/";
			}
		}

		public IActionResult Index() => View();

		public IActionResult IndexNoJS() => View();

		[HttpPost]
		public async Task<IActionResult> Calculate(CheckWriterViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.AmountInWords = await ApiClientFactory.Instance.CurrencyToWords(model.CheckAmount, inUpperCase: true);
				return View("Process", model);
			}
			else
			{
				return View();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
