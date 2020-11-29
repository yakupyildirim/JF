using System.Threading.Tasks;
public interface IDomainEventService
{
	Task Publish(DomainEvent domainEvent);
}