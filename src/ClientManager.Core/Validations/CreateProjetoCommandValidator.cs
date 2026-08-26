using ClientManager.Core.CQRS.Projetos.Commands;
using FluentValidation;

namespace ClientManager.Core.Validations;

public class CreateProjetoCommandValidator : AbstractValidator<CreateProjetoCommand>
{
    public CreateProjetoCommandValidator()
    {
        RuleFor(p => p.Titulo)
            .NotEmpty().WithMessage("O título do projeto é obrigatório.")
            .Length(5, 150).WithMessage("O título deve ter entre 5 e 150 caracteres.");

        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição é obrigatória.");

        RuleFor(p => p.Orcamento)
            .GreaterThan(0).WithMessage("O orçamento deve ser maior que zero.");
    }
}
