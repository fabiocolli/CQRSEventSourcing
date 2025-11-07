namespace Dominio.Base
{
	public abstract class EventoBase
	{
		public Guid Id { get; } = Guid.NewGuid();
		public DateTimeOffset DataHoraUtc { get; } = DateTimeOffset.UtcNow;
		public string Tipo => GetType().Name;
		public int Versao { get; set; } = 0;
	}
}
