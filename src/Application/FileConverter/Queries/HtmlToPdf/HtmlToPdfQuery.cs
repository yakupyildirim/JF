using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.FileConverter.Queries.HtmlToPdf
{
	public class HtmlToPdfQuery : IRequest<Result>
	{
		public string page { get; set; }
	}

	public class HtmlToPdfQueryHandler : IRequestHandler<HtmlToPdfQuery, Result>
	{
		private readonly IFileConverter _fileconverter;

		public HtmlToPdfQueryHandler(IFileConverter fileconverter, IMapper mapper)
		{
			_fileconverter = fileconverter;
		}

		public async Task<Result> Handle(HtmlToPdfQuery request, CancellationToken cancellationToken)
		{
			return await _fileconverter.Convert(request.page);
		}

	}
}
