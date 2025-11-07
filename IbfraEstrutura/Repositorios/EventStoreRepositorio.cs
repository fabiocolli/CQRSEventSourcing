using Dominio.Base;
using Dominio.Interfaces;
using InfraEstrutura.ContextoContexto;
using InfraEstrutura.EventSourcing;
using System.Data.Entity;
using System.Text.Json;

namespace InfraEstrutura.Repositorios
{
	public class EventStoreRepositorio : IEventStore
	{
		private readonly Contexto _contexto;

		public EventStoreRepositorio(Contexto contexto)
		{
			_contexto = contexto;
		}

		public async Task<IEnumerable<string>> ObterEventosRawAsync(Guid aggregateId)
		{
			return await _contexto.EventStore
				.AsNoTracking()
				.Where(e => e.AggregateId == aggregateId)
				.OrderBy(e => e.DataHoraUtc)
				.Select(e => e.Payload)
				.ToListAsync();
		}

		public async Task SalvarEventosAsync(Guid aggregateId, IEnumerable<EventoBase> eventos)
		{
			foreach (var e in eventos)
			{
				var armazenado = new EventoArmazenado
				{
					AggregateId = aggregateId,
					Tipo = e.Tipo,
					Payload = JsonSerializer.Serialize(e, e.GetType()),
					DataHoraUtc = e.DataHoraUtc,
					Versao = e.Versao
				};

				_contexto.EventStore.Add(armazenado);
			}

			await _contexto.SaveChangesAsync();
		}
	}
}
