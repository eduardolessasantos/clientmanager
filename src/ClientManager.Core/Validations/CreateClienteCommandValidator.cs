using ClientManager.Core.CQRS.Clientes.Commands;
using FluentValidation;

namespace ClientManager.Core.Validations;

public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteCommandValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Informe um e-mail válido.");

        RuleFor(c => c.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.");

        RuleFor(c => c.CPF)
            .NotEmpty().WithMessage("O CPF é obrigatório.");

        RuleFor(c => c.Endereco)
            .NotEmpty().WithMessage("O endereço é obrigatório.");
    }
}
