using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record EnviarPropostaCommand(Guid ProjetoId, decimal Valor, string Descricao, DateTime DataValidade) : IRequest<Guid>;

public class EnviarPropostaCommandHandler : IRequestHandler<EnviarPropostaCommand, Guid>
{
    private readonly IProjetoDomainRepository _repository;

    public EnviarPropostaCommandHandler(IProjetoDomainRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(EnviarPropostaCommand request, CancellationToken cancellationToken)
    {
        var proposta = new Proposta
        {
            ProjetoId = request.ProjetoId,
            Valor = request.Valor,
            Descricao = request.Descricao,
            DataValidade = request.DataValidade,
            Status = "Enviada",
            DataEnvio = DateTime.UtcNow
        };

        await _repository.AddPropostaAsync(proposta);
        return proposta.Id;
    }
}
