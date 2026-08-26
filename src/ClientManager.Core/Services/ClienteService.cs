using ClientManager.Core.CQRS.Clientes.Commands;
using ClientManager.Core.CQRS.Clientes.Queries;
using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.Services;

public class ClienteService : IClienteService
{
    private readonly IMediator _mediator;

    public ClienteService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Guid> CriarClienteAsync(Cliente cliente)
    {
        var command = new CreateClienteCommand(
            cliente.Nome,
            cliente.Email,
            cliente.Telefone,
            cliente.CPF,
            cliente.RG,
            cliente.CNH,
            cliente.Endereco,
            cliente.Bairro,
            cliente.Cidade,
            cliente.Estado,
            cliente.CEP
        );
        return await _mediator.Send(command);
    }

    public async Task EditarClienteAsync(Cliente cliente)
    {
        var command = new UpdateClienteCommand(
            cliente.Id,
            cliente.Nome,
            cliente.Email,
            cliente.Telefone,
            cliente.CPF,
            cliente.RG,
            cliente.CNH,
            cliente.Endereco,
            cliente.Bairro,
            cliente.Cidade,
            cliente.Estado,
            cliente.CEP
        );
        await _mediator.Send(command);
    }

    public async Task ExcluirClienteAsync(Guid id)
    {
        await _mediator.Send(new DeleteClienteCommand(id));
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        return await _mediator.Send(new GetClienteByIdQuery(id));
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        return await _mediator.Send(new GetAllClientesQuery());
    }
}
