using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Clientes.Commands;

public record DeleteClienteCommand(Guid Id) : IRequest<bool>;

public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, bool>
{
    private readonly IClienteRepository _repository;

    public DeleteClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id);
        if (cliente == null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}
