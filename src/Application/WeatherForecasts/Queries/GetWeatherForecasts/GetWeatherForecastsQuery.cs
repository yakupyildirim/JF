using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts
{
	public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDto>>
	{
	}

	public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;
		public GetWeatherForecastsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<WeatherForecastDto>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
		{
			return await _context.WeatherForecasts
				.OrderBy(x => x.Date)
				.ProjectTo<WeatherForecastDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

		}
	}
}
