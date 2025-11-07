using Aplicacao.Comandos;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacao.ManipuladoresDeComandos
{
	public class PaisManipuladorDeComando
	{
		private readonly IPais _paisRepositorio;
		private readonly IEventStore _eventStore;

		public PaisManipuladorDeComando(IPais paisRepositorio, IEventStore eventStore)
		{
			_paisRepositorio = paisRepositorio;
			_eventStore = eventStore;
		}

		public async Task<Guid> HandleAsync(CriarPaisComando command)
		{
			var dto = command.Pais;
			var pais = Pais.Criar(dto.Nome, dto.Sigla);

			// Persistir projeção (tabela Pais) - para consultas rápidas
			await _paisRepositorio.AdicionarAsync(pais);

			// Persistir eventos no event store
			await _eventStore.SalvarEventosAsync(pais.Id, pais.Eventos);

			// Limpar eventos após salvar (bom para evitar reenvio)
			pais.LimparEventos();

			return pais.Id;
		}

		public async Task HandleAsync(AtualizarPaisComando command)
		{
			var existente = await _paisRepositorio.ObterPorIdAsync(command.Id);

			if (existente == null)
				throw new Exception("País não encontrado.");

			existente.Atualizar(command.Nome, command.Sigla);

			await _paisRepositorio.AtualizarAsync(existente);

			await _eventStore.SalvarEventosAsync(existente.Id, existente.Eventos);

			existente.LimparEventos();
		}
	}
}
