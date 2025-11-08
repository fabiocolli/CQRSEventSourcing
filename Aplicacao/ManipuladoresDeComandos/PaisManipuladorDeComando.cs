using Aplicacao.Comandos;
using Aplicacao.Comum;
using Dominio.Entidades;
using Dominio.Interfaces;
using FluentValidation;

namespace Aplicacao.ManipuladoresDeComandos
{
	public class PaisManipuladorDeComando
	{
		private readonly IPais _paisRepositorio;
		private readonly IEventStore _eventStore;
		private readonly IValidator<Pais> _paisValidador;

		public PaisManipuladorDeComando(IPais paisRepositorio, IEventStore eventStore,
			IValidator<Pais> paisValidador)
		{
			_paisRepositorio = paisRepositorio;
			_eventStore = eventStore;
			_paisValidador = paisValidador;
		}

		public async Task<ResultadoComando<Pais>> HandleAsync(CriarPaisComando comando)
		{
			var dto = comando.Pais;
			var pais = Pais.Criar(dto.Nome, dto.Sigla);

			var resultadoValidacao = await _paisValidador.ValidateAsync(pais,
				regras => regras.IncludeAllRuleSets());

			if (!resultadoValidacao.IsValid)
			{
				return ResultadoComando<Pais>
					.Falha(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
			}

			// Persistir projeção (tabela Pais) - para consultas rápidas
			await _paisRepositorio.AdicionarAsync(pais);

			// Persistir eventos no event store
			await _eventStore.SalvarEventosAssincrono(pais.Id, pais.Eventos);

			// Limpar eventos após salvar (bom para evitar reenvio)
			pais.LimparEventos();

			return ResultadoComando<Pais>.Ok(pais);
		}

		public async Task<ResultadoComando<Pais>> HandleAsync(AtualizarPaisComando comando)
		{
			var existente = await _paisRepositorio.ObterPorIdAsync(comando.Id);

			if (existente == null)
				return ResultadoComando<Pais>.Falha(new[] { "País não encontrado." });

			var resultadoValidacao = await _paisValidador.ValidateAsync(
				Pais.Criar(comando.Nome, comando.Sigla),
				regras => regras.IncludeAllRuleSets());

			if (!resultadoValidacao.IsValid)
			{
				return ResultadoComando<Pais>
					.Falha(resultadoValidacao.Errors.Select(e => e.ErrorMessage));
			}

			existente.Atualizar(comando.Nome, comando.Sigla);

			await _paisRepositorio.AtualizarAsync(existente);

			await _eventStore.SalvarEventosAssincrono(existente.Id, existente.Eventos);

			existente.LimparEventos();

			return ResultadoComando<Pais>.Ok(existente);
		}
	}
}
