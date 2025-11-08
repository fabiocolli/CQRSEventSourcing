namespace InfraEstrutura.EventSourcing
{
	public class EventoArmazenado
	{
		public long Id { get; set; }
		public Guid AggregateId { get; set; }
		public string Tipo { get; set; }
		public string Payload { get; set; }
		public DateTimeOffset DataHoraUtc { get; set; } = DateTimeOffset.UtcNow;
		public int Versao { get; set; } = 1;
	}
}
