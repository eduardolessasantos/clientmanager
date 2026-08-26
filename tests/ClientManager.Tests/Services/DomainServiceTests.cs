using ClientManager.Core.Entities;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Services;
using MediatR;
using Moq;
using Xunit;

namespace ClientManager.Tests.Services;

public class DomainServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ClienteService _clienteService;
    private readonly ProjetoService _projetoService;

    public DomainServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _clienteService = new ClienteService(_mediatorMock.Object);
        _projetoService = new ProjetoService(_mediatorMock.Object);
    }

    [Fact]
    public async Task CriarClienteAsync_ComClienteValido_DeveRetornarGuidEEnviarCommand()
    {
        // Arrange
        var cliente = new Cliente
        {
            Nome = "Cliente Moq Test",
            Email = "moq@teste.com",
            Telefone = "11999990000"
        };
        var expectedId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), default))
                     .ReturnsAsync(expectedId);

        // Act
        var result = await _clienteService.CriarClienteAsync(cliente);

        // Assert
        Assert.Equal(expectedId, result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<Guid>>(), default), Times.Once);
    }

    [Fact]
    public async Task CriarProjetoAsync_ComRegrasDeNegocioValidas_DeveRetornarGuid()
    {
        // Arrange
        var projeto = new Projeto
        {
            Titulo = "Novo Sistema Web",
            Descricao = "Sistema de Gestão de Clientes e Projetos",
            Orcamento = 150000.00m,
            DataInicio = DateTime.UtcNow
        };
        var expectedId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), default))
                     .ReturnsAsync(expectedId);

        // Act
        var result = await _projetoService.CriarProjetoAsync(projeto);

        // Assert
        Assert.Equal(expectedId, result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<Guid>>(), default), Times.Once);
    }

    [Fact]
    public async Task EnviarPropostaAsync_ComValorValido_DeveChamarMediator()
    {
        // Arrange
        var proposta = new Proposta
        {
            ProjetoId = Guid.NewGuid(),
            Valor = 25000.00m,
            Descricao = "Proposta de consultoria técnica",
            DataValidade = DateTime.UtcNow.AddDays(30)
        };
        var expectedId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<Guid>>(), default))
                     .ReturnsAsync(expectedId);

        // Act
        var result = await _projetoService.EnviarPropostaAsync(proposta);

        // Assert
        Assert.Equal(expectedId, result);
        _mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<Guid>>(), default), Times.Once);
    }
}
