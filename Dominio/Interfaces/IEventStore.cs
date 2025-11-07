
using Dominio.Base;

namespace Dominio.Interfaces
{
	public interface IEventStore
	{
		Task SalvarEventosAsync(Guid aggregateId, IEnumerable<EventoBase> eventos);
		Task<IEnumerable<string>> ObterEventosRawAsync(Guid aggregateId);
	}
}
