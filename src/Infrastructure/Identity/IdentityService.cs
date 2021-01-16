using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public IdentityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<string> GetUserNameAsync(string userId)
		{
			var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

			return user.UserName;
		}
		public async Task<Result> CreateUserAsync(string userName, string email, string password)
		{

			var user = new ApplicationUser
			{
				UserName = userName,
				Email = email,
			};

			var result = await _userManager.CreateAsync(user, password);

			await _userManager.AddClaimAsync(user, new Claim("userName", user.UserName));
			await _userManager.AddClaimAsync(user, new Claim("email", user.Email));
			await _userManager.AddClaimAsync(user, new Claim("role", "user"));

			if (result.Succeeded)
			{
				Result.Data = user.Id;
				return Result.Success();
			}
			else
			{
				return Result.Failure(result.Errors.Select(e => e.Description));
			}
		}

		public async Task<Result> DeleteUserAsync(string userId)
		{
			var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

			if (user != null)
			{
				return await DeleteUserAsync(user);
			}

			return Result.Success();
		}

		public async Task<Result> DeleteUserAsync(ApplicationUser user)
		{
			var result = await _userManager.DeleteAsync(user);

			return result.ToApplicationResult();
		}
	}
}
