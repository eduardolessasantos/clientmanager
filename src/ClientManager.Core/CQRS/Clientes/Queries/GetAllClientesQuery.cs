using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Clientes.Queries;

public record GetAllClientesQuery() : IRequest<IEnumerable<Cliente>>;

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<Cliente>>
{
    private readonly IClienteRepository _repository;

    public GetAllClientesQueryHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Cliente>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
