using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Common.Context
{
	public class CommunicationContext
	{
		IMessage _message;
		ICommunication _communicationSender;
		public CommunicationContext(ICommunication communicationSender, IMessage message)//Adapter pattern
		{
			_communicationSender = communicationSender;
			_message = message;
		}
		public void Send()
		{
			_communicationSender.Send(_message);
		}
	}
}