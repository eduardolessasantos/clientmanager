using ClientManager.Core.CQRS.Projetos.Commands;
using FluentValidation;

namespace ClientManager.Core.Validations;

public class MudarStatusPropostaCommandValidator : AbstractValidator<MudarStatusPropostaCommand>
{
    private static readonly string[] StatusValidos = { "Enviada", "Aceita", "Rejeitada", "Cancelada" };

    public MudarStatusPropostaCommandValidator()
    {
        RuleFor(s => s.PropostaId)
            .NotEmpty().WithMessage("O ID da proposta é obrigatório.");

        RuleFor(s => s.NovoStatus)
            .NotEmpty().WithMessage("O novo status é obrigatório.")
            .Must(status => StatusValidos.Contains(status))
            .WithMessage("Status inválido. Status permitidos: Enviada, Aceita, Rejeitada, Cancelada.");
    }
}
