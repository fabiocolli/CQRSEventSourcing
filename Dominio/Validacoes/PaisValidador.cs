using Dominio.Entidades;
using FluentValidation;

namespace Dominio.Validacoes
{
	public class PaisValidador : AbstractValidator<Pais>
	{
		public PaisValidador()
		{
			RuleSet("Nomes", () =>
			{
				RuleFor(p => p.Nome)
					.NotEmpty()
					.WithSeverity(Severity.Warning)
					.WithName("Nome do País")
					.MaximumLength(100)
					.WithMessage("O nome não pode exceder 100 caracteres.")
					.MinimumLength(5)
					.WithMessage("O nome deve ter pelo menos 5 caracteres.");

				RuleFor(p => p.Nome)
					.Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$")
					.WithSeverity(Severity.Error)
					.WithMessage("O nome deve conter apenas letras e espaços.");
			});

			RuleSet("Siglas", () =>
			{
				RuleFor(p => p.Sigla)
					.NotEmpty()
					.WithSeverity(Severity.Warning)
					.WithName("Sigla do País")
					.MaximumLength(3)
					.WithMessage("A sigla não pode exceder 3 caracteres.")
					.MinimumLength(2)
					.WithMessage("A sigla deve ter pelo menos 1 caracteres.");

				RuleFor(p => p.Sigla)
					.Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$")
					.WithSeverity(Severity.Error)
					.WithMessage("A sigla deve conter apenas letras e espaços.");
			});

		}
	}
}
