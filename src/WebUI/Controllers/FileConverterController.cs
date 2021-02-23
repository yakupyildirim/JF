using System.Threading.Tasks;
using CleanArchitecture.Application.FileConverter.Queries.HtmlToPdf;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
	public class FileConverterController : ApiController
	{

		[HttpPost("/api/fileconverter/htmltopdf")]
		public async Task<ActionResult> HtmlToPdf(HtmlToPdfQuery query)
		{
			var result = await Mediator.Send(query);
			return File((byte[])result.Data, "application/pdf");
		}

	}
}