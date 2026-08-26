using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Queries;

public record GetProjetoByIdQuery(Guid Id) : IRequest<Projeto?>;

public class GetProjetoByIdQueryHandler : IRequestHandler<GetProjetoByIdQuery, Projeto?>
{
    private readonly IProjetoDomainRepository _repository;

    public GetProjetoByIdQueryHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<Projeto?> Handle(GetProjetoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
