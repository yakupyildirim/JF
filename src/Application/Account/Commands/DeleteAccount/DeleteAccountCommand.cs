using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Account.Commands.CreateAccount
{
	public class DeleteAccountCommand : IRequest<Result>
	{
		public string userId { get; set; }
	}
	public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
	{
		private readonly IIdentityService _identityService;

		public DeleteAccountCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
		{
			return _identityService.DeleteUserAsync(request.userId);
		}
	}
}