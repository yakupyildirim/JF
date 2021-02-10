using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces
{
	public abstract class ICommunication
	{
		public virtual Task<Result> Send(IMessage message)
		{
			return null;
		}
		public virtual Task<Result> Send()
		{
			return null;
		}
	}
}