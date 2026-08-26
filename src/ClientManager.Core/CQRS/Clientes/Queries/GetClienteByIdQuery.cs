using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Clientes.Queries;

public record GetClienteByIdQuery(Guid Id) : IRequest<Cliente?>;

public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, Cliente?>
{
    private readonly IClienteRepository _repository;

    public GetClienteByIdQueryHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cliente?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
