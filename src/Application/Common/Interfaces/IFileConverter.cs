using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces
{
	public abstract class IFileConverter
	{
		public abstract Task<Result> Convert(string page);
	}
}