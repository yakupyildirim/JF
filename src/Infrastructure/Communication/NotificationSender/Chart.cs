
using System.Collections.Generic;

namespace CleanArchitecture.Infrastructure.Communication.NotificationSender
{
	public class Chart
	{
		public List<int> Data { get; set; }
		public string Label { get; set; }

		public Chart()
		{
			Data = new List<int>();
		}
	}
}