using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record UpdateProjetoCommand(
    Guid Id,
    string Titulo,
    string Descricao,
    string Status,
    decimal Orcamento,
    DateTime DataInicio,
    DateTime? DataFim
) : IRequest<bool>;

public class UpdateProjetoCommandHandler : IRequestHandler<UpdateProjetoCommand, bool>
{
    private readonly IProjetoDomainRepository _repository;

    public UpdateProjetoCommandHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateProjetoCommand request, CancellationToken cancellationToken)
    {
        var projeto = await _repository.GetByIdAsync(request.Id);
        if (projeto == null) return false;

        projeto.Titulo = request.Titulo;
        projeto.Descricao = request.Descricao;
        projeto.Status = request.Status;
        projeto.Orcamento = request.Orcamento;
        projeto.DataInicio = request.DataInicio;
        projeto.DataFim = request.DataFim;

        await _repository.UpdateAsync(projeto);
        return true;
    }
}
