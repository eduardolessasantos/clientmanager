using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Queries;

public record GetAllProjetosQuery() : IRequest<IEnumerable<Projeto>>;

public class GetAllProjetosQueryHandler : IRequestHandler<GetAllProjetosQuery, IEnumerable<Projeto>>
{
    private readonly IProjetoDomainRepository _repository;

    public GetAllProjetosQueryHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Projeto>> Handle(GetAllProjetosQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
