using Dominio.Base;

namespace Dominio.Interfaces
{
	public interface IEventStore
	{
		Task SalvarEventosAssincrono(Guid idAgregado, IEnumerable<EventoBase> eventos);
		Task<IEnumerable<string>> ObterEventosAssincrono(Guid idAgregado);
	}
}
