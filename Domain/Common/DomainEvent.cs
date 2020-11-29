using System;
using System.Collections.Generic;

public abstract class DomainEvent
{
	protected DomainEvent()
	{
		DateOccured = DateTimeOffset.UtcNow;
	}

	public DateTimeOffset DateOccured { get; protected set; } = DateTime.UtcNow;
}

public interface IHasDomainEvent
{
	public List<DomainEvent> DomainEvents { get; set; }
}