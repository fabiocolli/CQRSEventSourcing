using Dominio.Base;
using Dominio.Entidades;
using InfraEstrutura.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace InfraEstrutura.ContextoContexto
{
	public class Contexto : DbContext
	{
		public DbSet<Pais> Pais { get; set; }
		public DbSet<EventoArmazenado> EventStore { get; set; }

		public Contexto(DbContextOptions<Contexto> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<EventoBase>();

			// Mapeamento simples para Pais (projeção)
			modelBuilder.Entity<Pais>()
				.ToTable("Paises")
				.HasKey(k => k.Id);

			modelBuilder.Entity<Pais>(b =>
			{

				b.Property<string>("Nome")
					.IsRequired()
					.HasMaxLength(100);

				b.Property<string>("Sigla")
					.IsRequired()
					.HasMaxLength(3);
			});

			// Mapeamento para EventStore
			modelBuilder.Entity<EventoArmazenado>()
				.ToTable("EventStore")
				.HasKey(k => k.Id);

			modelBuilder.Entity<EventoArmazenado>(b =>
			{
				b.Property(e => e.AggregateId)
					.IsRequired();

				b.Property(e => e.DataHoraUtc)
					.IsRequired();

				b.Property(e => e.Tipo)
					.IsRequired()
					.HasMaxLength(200);

				b.Property(e => e.Payload)
					.IsRequired(); // JSON payload

				b.Property(e => e.Versao)
					.IsRequired();
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
