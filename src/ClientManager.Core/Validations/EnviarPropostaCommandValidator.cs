using ClientManager.Core.CQRS.Projetos.Commands;
using FluentValidation;

namespace ClientManager.Core.Validations;

public class EnviarPropostaCommandValidator : AbstractValidator<EnviarPropostaCommand>
{
    public EnviarPropostaCommandValidator()
    {
        RuleFor(p => p.ProjetoId)
            .NotEmpty().WithMessage("O ID do projeto é obrigatório.");

        RuleFor(p => p.Valor)
            .GreaterThan(0).WithMessage("O valor da proposta deve ser maior que zero.");

        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição da proposta é obrigatória.");

        RuleFor(p => p.DataValidade)
            .GreaterThan(DateTime.UtcNow.AddMinutes(-5)).WithMessage("A data de validade deve ser no futuro.");
    }
}
