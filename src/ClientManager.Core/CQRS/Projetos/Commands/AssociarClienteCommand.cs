using ClientManager.Core.Interfaces;
using MediatR;

namespace ClientManager.Core.CQRS.Projetos.Commands;

public record AssociarClienteCommand(Guid ProjetoId, Guid ClienteId) : IRequest<bool>;

public class AssociarClienteCommandHandler : IRequestHandler<AssociarClienteCommand, bool>
{
    private readonly IProjetoDomainRepository _projetoRepository;
    private readonly IClienteRepository _clienteRepository;

    public AssociarClienteCommandHandler(IProjetoDomainRepository projetoRepository, IClienteRepository clienteRepository)
    {
        _projetoRepository = projetoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<bool> Handle(AssociarClienteCommand request, CancellationToken cancellationToken)
    {
        var projeto = await _projetoRepository.GetByIdAsync(request.ProjetoId);
        if (projeto == null) return false;

        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId);
        if (cliente == null) return false;

        projeto.ClienteId = cliente.Id;
        projeto.Cliente = cliente;

        await _projetoRepository.UpdateAsync(projeto);
        return true;
    }
}
