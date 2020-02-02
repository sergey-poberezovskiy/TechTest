using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTest.Web.Services;

namespace TechTest.Web.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class CheckWriterController : ControllerBase
	{
		private readonly ICheckWriterService _checkWriterService;

		public CheckWriterController(ICheckWriterService checkWriterService)
		{
			_checkWriterService = checkWriterService;
		}

		[HttpGet("{value}/{inUpperCase?}")]
		public async Task<ActionResult<string>> NumberToWords(double? value, bool? inUpperCase)
		{
			ActionResult result;
			if (value.HasValue && value.Value >= 0)
			{
				try
				{
					result = Ok(_checkWriterService.CurrencyToWords(value.Value, inUpperCase: inUpperCase ?? false));
				}
				catch (Exception ex)
				{
					result = BadRequest(ex.Message);
				}
			}
			else
			{
				result = BadRequest("Check value cannot be negative.");
			}

			return await Task.FromResult(result);
		}
	}
}