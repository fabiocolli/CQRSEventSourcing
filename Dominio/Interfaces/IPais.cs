using Dominio.Entidades;

namespace Dominio.Interfaces
{
	public interface IPais
	{
		Task<Pais> ObterPorIdAsync(Guid id);
		Task<Pais> ObterPorSiglaAsync(string sigla);
		Task<IEnumerable<Pais>> ObterTodosAsync();
		Task AdicionarAsync(Pais pais);
		Task AtualizarAsync(Pais pais);
		Task RemoverAsync(Guid id);
	}
}
