using MediatR;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
	public DomainEventNotification(TDomainEvent domainEvent)
	{
		DomainEvent = domainEvent;
	}

	public TDomainEvent DomainEvent { get; }
}