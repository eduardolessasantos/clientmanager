using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Clientes.Commands;

public record UpdateClienteCommand(
    Guid Id,
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
) : IRequest<Unit>;

public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, Unit>
{
    private readonly IClienteRepository _repository;

    public UpdateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id);
        if (cliente == null)
            throw new KeyNotFoundException($"Cliente com ID {request.Id} não encontrado.");

        cliente.Nome = request.Nome;
        cliente.Email = request.Email;
        cliente.Telefone = request.Telefone;
        cliente.CPF = request.CPF;
        cliente.RG = request.RG;
        cliente.CNH = request.CNH;
        cliente.Endereco = request.Endereco;
        cliente.Bairro = request.Bairro;
        cliente.Cidade = request.Cidade;
        cliente.Estado = request.Estado;
        cliente.CEP = request.CEP;

        await _repository.UpdateAsync(cliente);
        return Unit.Value;
    }
}
