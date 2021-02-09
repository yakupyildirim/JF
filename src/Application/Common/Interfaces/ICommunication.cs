using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Common.Interfaces
{
	public abstract class ICommunication
	{
		public virtual Task<Result> Send(IMessage message)
		{
			throw new NotImplementedException();	
		}
		public virtual Task<Result> Send()
		{
			throw new NotImplementedException();
		}
	}
}