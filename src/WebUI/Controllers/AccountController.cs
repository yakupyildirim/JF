using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;

		}


		[HttpPost("/api/user/register")]
		public async Task<(Result Result, string UserId)> Register([FromBody] RegisterModel request)
		{
			IdentityService service = new IdentityService(_userManager);
			var result = service.CreateUserAsync(request.Email, request.Password);
			return await result;
	
		}

	}

	public class RegisterModel
	{
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}