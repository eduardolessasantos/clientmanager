using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record DeleteProjetoCommand(Guid Id) : IRequest<bool>;

public class DeleteProjetoCommandHandler : IRequestHandler<DeleteProjetoCommand, bool>
{
    private readonly IProjetoDomainRepository _repository;

    public DeleteProjetoCommandHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = await _repository.GetByIdAsync(request.Id);
        if (projeto == null) return false;

        await _repository.DeleteAsync(request.Id);
        return true;
    }
}
