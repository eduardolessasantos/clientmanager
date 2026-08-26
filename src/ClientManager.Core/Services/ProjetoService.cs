using ClientManager.Core.CQRS.Projetos.Commands;
using ClientManager.Core.CQRS.Projetos.Queries;
using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.Services;

public class ProjetoService : IProjetoService
{
    private readonly IMediator _mediator;

    public ProjetoService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Guid> CriarProjetoAsync(Projeto projeto)
    {
        var command = new CreateProjetoCommand(
            projeto.Titulo,
            projeto.Descricao,
            projeto.Orcamento,
            projeto.DataInicio,
            projeto.DataFim,
            projeto.ClienteId,
            projeto.CategoriaId
        );
        return await _mediator.Send(command);
    }

    public async Task EditarProjetoAsync(Projeto projeto)
    {
        var command = new UpdateProjetoCommand(
            projeto.Id,
            projeto.Titulo,
            projeto.Descricao,
            projeto.Status,
            projeto.Orcamento,
            projeto.DataInicio,
            projeto.DataFim
        );
        await _mediator.Send(command);
    }

    public async Task ExcluirProjetoAsync(Guid id)
    {
        var command = new DeleteProjetoCommand(id);
        await _mediator.Send(command);
    }

    public async Task AssociarClienteAsync(Guid projetoId, Guid clienteId)
    {
        var command = new AssociarClienteCommand(projetoId, clienteId);
        await _mediator.Send(command);
    }

    public async Task<Guid> EnviarPropostaAsync(Proposta proposta)
    {
        var command = new EnviarPropostaCommand(
            proposta.ProjetoId,
            proposta.Valor,
            proposta.Descricao,
            proposta.DataValidade
        );
        return await _mediator.Send(command);
    }

    public async Task MudarStatusPropostaAsync(Guid propostaId, string novoStatus)
    {
        var command = new MudarStatusPropostaCommand(propostaId, novoStatus);
        await _mediator.Send(command);
    }

    public async Task<Projeto?> ObterPorIdAsync(Guid id)
    {
        return await _mediator.Send(new GetProjetoByIdQuery(id));
    }

    public async Task<IEnumerable<Projeto>> ObterTodosAsync()
    {
        return await _mediator.Send(new GetAllProjetosQuery());
    }
}
