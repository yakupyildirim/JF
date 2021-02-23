
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using System.IO;
using CleanArchitecture.Application.Common.Models;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace CleanArchitecture.Infrastructure.FileConverter.HtmlToPdf
{
	public class HtmlToPdfService : IFileConverter
	{
		private IConverter _converter;
		public HtmlToPdfService(IConverter converter)
		{
			_converter = converter;
		}
		public override async Task<Result> Convert(string page)
		{
			var file = _converter.Convert(GetHtmlToPdfDocument(page));

			await Task.Delay(1);
			return Result.Success(file);
		}

		private HtmlToPdfDocument GetHtmlToPdfDocument(string page)
		{
			var globalSettings = new GlobalSettings
			{
				ColorMode = ColorMode.Color,
				Orientation = Orientation.Portrait,
				PaperSize = PaperKind.A4,
				Margins = new MarginSettings { Top = 10 },
				DocumentTitle = "PDF Report",
				//Out = @"D:\PDFCreator\Employee_Report.pdf"  USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
			};

			var objectSettings = new ObjectSettings
			{
				PagesCount = true,
				//HtmlContent = TemplateGenerator.GetHTMLString(),
				HtmlContent = page, // "https://code-maze.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
				WebSettings = { DefaultEncoding = "utf-8"}, // UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") 
				HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
				FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
			};

			var pdf = new HtmlToPdfDocument()
			{
				GlobalSettings = globalSettings,
				Objects = { objectSettings }
			};

			return pdf;
		}
	}
}