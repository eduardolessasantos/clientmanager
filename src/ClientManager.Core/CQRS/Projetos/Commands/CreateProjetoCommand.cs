using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record CreateProjetoCommand(
    string Titulo,
    string Descricao,
    decimal Orcamento,
    DateTime StartDate,
    DateTime? EndDate,
    Guid? ClienteId,
    Guid? CategoriaId
) : IRequest<Guid>;

public class CreateProjetoCommandHandler : IRequestHandler<CreateProjetoCommand, Guid>
{
    private readonly IProjetoDomainRepository _repository;

    public CreateProjetoCommandHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = new Projeto
        {
            Titulo = request.Titulo,
            Descricao = request.Descricao,
            Orcamento = request.Orcamento,
            DataInicio = request.StartDate,
            DataFim = request.EndDate,
            ClienteId = request.ClienteId,
            CategoriaId = request.CategoriaId
        };

        await _repository.AddAsync(projeto);
        await _repository.SaveChangesAsync();
        return projeto.Id;
    }
}
