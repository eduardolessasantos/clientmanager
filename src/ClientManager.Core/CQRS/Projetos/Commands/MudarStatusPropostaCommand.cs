using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record MudarStatusPropostaCommand(Guid PropostaId, string NovoStatus) : IRequest<bool>;

public class MudarStatusPropostaCommandHandler : IRequestHandler<MudarStatusPropostaCommand, bool>
{
    private readonly IProjetoDomainRepository _repository;

    public MudarStatusPropostaCommandHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(MudarStatusPropostaCommand request, CancellationToken cancellationToken)
    {
        var proposta = await _repository.GetPropostaByIdAsync(request.PropostaId);
        if (proposta == null) return false;

        proposta.Status = request.NovoStatus;
        await _repository.UpdatePropostaAsync(proposta);
        return true;
    }
}
