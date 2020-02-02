using System;
using System.ComponentModel.DataAnnotations;

namespace TechTest.Web.Models
{
	public class CheckWriterViewModel
	{
		[Display(Name = "Person Name")]
		[Required]
		public string PersonName { get; set; }

		[Display(Name = "Check Amount")]
		[Range(0, 1000000000000000)]
		public double CheckAmount { get; set; }

		[Display(Name ="Amount in words")]
		public string AmountInWords { get; set; }
	}
}
