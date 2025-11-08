using Dominio.Entidades;
using Dominio.Interfaces;
using InfraEstrutura.ContextoContexto;
using Microsoft.EntityFrameworkCore;

namespace InfraEstrutura.Repositorios
{
	public class PaisRepositorio : IPais
	{
		private readonly Contexto _context;

		public PaisRepositorio(Contexto context)
		{
			_context = context;
		}

		public async Task AdicionarAsync(Pais pais)
		{
			_context.Pais.Add(pais);
			await _context.SaveChangesAsync();
		}

		public async Task AtualizarAsync(Pais pais)
		{
			_context.Pais.Update(pais);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Pais>> ObterTodosAsync()
		{
			return await _context.Pais.AsNoTracking().ToListAsync();
		}

		public async Task<Pais> ObterPorIdAsync(Guid id)
		{
			return await _context.Pais.FindAsync(id);
		}

		public async Task<Pais> ObterPorSiglaAsync(string sigla)
		{
			return await _context.Pais.AsNoTracking()
				.FirstOrDefaultAsync(p => EF.Property<string>(p, "Sigla") == sigla);
		}

		public async Task RemoverAsync(Guid id)
		{
			var p = await _context.Pais.FindAsync(id);

			if (p != null)
			{
				_context.Pais.Remove(p);
				await _context.SaveChangesAsync();
			}
		}
	}
}
