using Dominio.Base;
using Dominio.Interfaces;

namespace Aplicacao.Servicos
{
	public class EventStoreServico
	{
		private readonly IEventStore _eventStore;

		public EventStoreServico(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async Task SalvarAsync(Guid aggregateId, IEnumerable<EventoBase> eventos)
		{
			await _eventStore.SalvarEventosAsync(aggregateId, eventos);
		}
	}
}
