using Dominio.Base;

namespace Dominio.Eventos
{
	public class PaisCriadoEvento : EventoBase
	{
		public string Nome { get; set; }
		public string Sigla { get; set; }

		public PaisCriadoEvento(string nome, string sigla)
		{
			Nome = nome;
			Sigla = sigla;
		}
	}
}
