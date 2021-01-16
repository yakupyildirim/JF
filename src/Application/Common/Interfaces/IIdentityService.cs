using CleanArchitecture.Application.Common.Models;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<Result> CreateUserAsync(string userName, string email, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
