using ClientManager.Core.CQRS.Clientes.Commands;
using ClientManager.Core.CQRS.Projetos.Commands;
using ClientManager.Core.Validations;
using Xunit;

namespace ClientManager.Tests.Validations;

public class ValidationTests
{
    [Fact]
    public void CreateClienteCommandValidator_ComCamposValidos_DevePassarNaValidacao()
    {
        // Arrange
        var validator = new CreateClienteCommandValidator();
        var command = new CreateClienteCommand("Cliente Silva", "silva@empresa.com", "11999998888", "123.456.789-00", "12.345.678-9", "12345678900", "Rua das Flores, 123", "Centro", "São Paulo", "SP", "01000-000");

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void CreateClienteCommandValidator_ComCamposInvalidos_DeveRetornarErros()
    {
        // Arrange
        var validator = new CreateClienteCommandValidator();
        var command = new CreateClienteCommand("", "email_invalido", "", "", null, null, "", null, null, null, null);

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Nome");
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
        Assert.Contains(result.Errors, e => e.PropertyName == "Telefone");
        Assert.Contains(result.Errors, e => e.PropertyName == "CPF");
        Assert.Contains(result.Errors, e => e.PropertyName == "Endereco");
    }

    [Fact]
    public void CreateProjetoCommandValidator_ComOrcamentoZero_DeveFalhar()
    {
        // Arrange
        var validator = new CreateProjetoCommandValidator();
        var command = new CreateProjetoCommand("Projeto X", "Descricao do projeto X", 0, DateTime.UtcNow, null, null, null);

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Orcamento");
    }

    [Fact]
    public void EnviarPropostaCommandValidator_ComValorInvalido_DeveFalhar()
    {
        // Arrange
        var validator = new EnviarPropostaCommandValidator();
        var command = new EnviarPropostaCommand(Guid.NewGuid(), -500, "", DateTime.UtcNow.AddDays(-1));

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Valor");
        Assert.Contains(result.Errors, e => e.PropertyName == "Descricao");
        Assert.Contains(result.Errors, e => e.PropertyName == "DataValidade");
    }
}
