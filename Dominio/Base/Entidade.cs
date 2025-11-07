namespace Dominio.Base
{
	public abstract class Entidade
	{
		public Guid Id { get; protected set; } = Guid.NewGuid();

		public int Versao { get; protected set; } = 0;

		private readonly List<EventoBase> _eventos = new List<EventoBase>();
		public IReadOnlyCollection<EventoBase> Eventos => _eventos.AsReadOnly();

		protected void AdicionarEvento(EventoBase evento)
		{
			Versao++;

			evento.Versao = Versao;

			_eventos.Add(evento);
		}

		public void LimparEventos() => _eventos.Clear();
	}
}
