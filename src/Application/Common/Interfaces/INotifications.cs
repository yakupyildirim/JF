using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Interfaces
{
	public interface INotifications
	{
		NotificationType NotificationType { get; set; }
	}
}
