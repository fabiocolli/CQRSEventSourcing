using Dominio.Base;

namespace Dominio.Eventos
{
	public class PaisAtualizadoEvento : EventoBase
	{
		public string Nome { get; set; }
		public string Sigla { get; set; }

		public PaisAtualizadoEvento(string nome, string sigla)
		{
			Nome = nome;
			Sigla = sigla;
		}
	}
}
