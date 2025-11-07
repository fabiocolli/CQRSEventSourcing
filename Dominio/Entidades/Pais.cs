using Dominio.Base;
using Dominio.Eventos;

namespace Dominio.Entidades
{
	public class Pais : Entidade
	{
		public string Nome { get; private set; }
		public string Sigla { get; private set; }

		private Pais() { }

		public static Pais Criar(string nome, string sigla)
		{
			//var validador = new PaisValidador();

			//var resultadoValidacao = validador.Validate(new Pais
			//{
			//	Nome = nome,
			//	Sigla = sigla
			//});

			var pais = new Pais();

			var @event = new PaisCriadoEvento(nome, sigla);

			pais.AplicarGenerico(@event);

			pais.AdicionarEvento(@event);

			return pais;
		}
		public void Atualizar(string nome, string sigla)
		{
			var @event = new PaisAtualizadoEvento(nome, sigla);

			AplicarGenerico(@event);

			AdicionarEvento(@event);
		}

		public void Aplicar(PaisCriadoEvento e)
		{
			Id = Id == Guid.Empty ? Guid.NewGuid() : Id;
			Nome = e.Nome;
			Sigla = e.Sigla;
		}

		public void Aplicar(PaisAtualizadoEvento e)
		{
			Nome = e.Nome;
			Sigla = e.Sigla;
		}

		private void AplicarGenerico(EventoBase e)
		{
			switch (e)
			{
				case PaisCriadoEvento pc:
				Aplicar(pc);
				break;
				case PaisAtualizadoEvento pa:
				Aplicar(pa);
				break;
			}
		}
	}
}