using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Account.Commands.CreateAccount
{
	public class CreateAccountCommand : IRequest<Result>
	{
		public string userName { get; set; }
		public string email { get; set; }
		public string password { get; set; }
	}
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public CreateAccountCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
		{
			return _identityService.CreateUserAsync(request.userName, request.email, request.password);
		}
	}
}