using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Models
{
  public class Notification<T> : INotifications
  {
    public NotificationType NotificationType { get; set; }
    public T Payload { get; set; }
  }
}