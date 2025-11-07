using Aplicacao.Comandos;
using Aplicacao.Dtos;
using Aplicacao.ManipuladoresDeComandos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PaisController : Controller
	{
		private readonly PaisManipuladorDeComando _handler;
		private readonly IPais _repositorio;

		public PaisController(PaisManipuladorDeComando handler, IPais repositorio)
		{
			_handler = handler;
			_repositorio = repositorio;
		}

		[Produces("application/json")]
		[HttpPost]
		public async Task<IActionResult> Criar([FromBody] PaisDTO dto)
		{
			var cmd = new CriarPaisComando { Pais = dto };

			var id = await _handler.HandleAsync(cmd);

			return CreatedAtAction(nameof(ObterPorId), new { id }, new { id });
		}

		[Produces("application/json")]
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Atualizar(Guid id, [FromBody] PaisDTO dto)
		{
			var cmd = new AtualizarPaisComando
			{
				Id = id,
				Nome = dto.Nome,
				Sigla = dto.Sigla
			};

			await _handler.HandleAsync(cmd);

			return NoContent();
		}

		[Produces("application/json")]
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> ObterPorId(Guid id)
		{
			var p = await _repositorio.ObterPorIdAsync(id);

			if (p == null)
				return NotFound();

			return Ok(new
			{
				p.Id,
				Nome = p.GetType().GetProperty("Nome").GetValue(p),
				Sigla = p.GetType().GetProperty("Sigla").GetValue(p)
			});
		}

		[Produces("application/json")]
		[HttpGet]
		public async Task<IActionResult> ObterTodos()
		{
			var todos = await _repositorio.ObterTodosAsync();

			return Ok(todos);
		}
	}
}
