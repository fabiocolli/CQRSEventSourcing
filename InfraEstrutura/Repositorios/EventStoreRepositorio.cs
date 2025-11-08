using Dominio.Base;
using Dominio.Interfaces;
using InfraEstrutura.Contextos;
using InfraEstrutura.EventSourcing;
using Microsoft.EntityFrameworkCore;
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

		public async Task<IEnumerable<string>> ObterEventosAssincrono(Guid idAgregado)
		{
			return await _contexto.EventStore
				.AsNoTracking()
				.Where(e => e.IdAgregado == idAgregado)
				.OrderBy(e => e.DataHoraUtc)
				.Select(e => e.Payload)
				.ToListAsync();
		}

		public async Task SalvarEventosAssincrono(Guid idAgregado, IEnumerable<EventoBase> eventos)
		{
			foreach (var e in eventos)
			{
				var armazenado = new EventoArmazenado
				{
					IdAgregado = idAgregado,
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
