using System.ComponentModel.DataAnnotations;

namespace ClientManager.Web.Models;

public class ClientInputModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    [Display(Name = "Nome Completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [Display(Name = "E-mail de Contato")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "Informe um número de telefone válido.")]
    [Display(Name = "Telefone / WhatsApp")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [Display(Name = "CPF")]
    public string CPF { get; set; } = string.Empty;

    [Display(Name = "RG")]
    public string? RG { get; set; }

    [Display(Name = "CNH")]
    public string? CNH { get; set; }

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    [Display(Name = "Endereço")]
    public string Endereco { get; set; } = string.Empty;

    [Display(Name = "Bairro")]
    public string? Bairro { get; set; }

    [Display(Name = "Cidade")]
    public string? Cidade { get; set; }

    [Display(Name = "UF / Estado")]
    public string? Estado { get; set; }

    [Display(Name = "CEP")]
    public string? CEP { get; set; }
}
