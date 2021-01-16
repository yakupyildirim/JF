using System.Threading.Tasks;
using CleanArchitecture.Application.Account.Commands.CreateAccount;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
	public class AccountController : ApiController
	{

		[HttpPost("/api/user/register")]
		public async Task<ActionResult<Result>> Register(CreateAccountCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPost("/api/user/delete")]
		public async Task<ActionResult<Result>> Delete(DeleteAccountCommand command)
		{
			return await Mediator.Send(command);
		}

	}
}