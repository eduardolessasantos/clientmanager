using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Clientes.Commands;

public record CreateClienteCommand(
    string Nome,
    string Email,
    string Telefone,
    string CPF,
    string? RG,
    string? CNH,
    string Endereco,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? CEP
) : IRequest<Guid>;

public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, Guid>
{
    private readonly IClienteRepository _repository;

    public CreateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Nome = request.Nome,
            Email = request.Email,
            Telefone = request.Telefone,
            CPF = request.CPF,
            RG = request.RG,
            CNH = request.CNH,
            Endereco = request.Endereco,
            Bairro = request.Bairro,
            Cidade = request.Cidade,
            Estado = request.Estado,
            CEP = request.CEP
        };

        await _repository.AddAsync(cliente);
        await _repository.SaveChangesAsync();
        return cliente.Id;
    }
}
