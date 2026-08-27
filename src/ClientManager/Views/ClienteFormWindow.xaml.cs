using System.Windows;
using ClientManager.Models;

namespace ClientManager.Views;

public partial class ClienteFormWindow : Window
{
    public Cliente Cliente { get; private set; }

    public ClienteFormWindow(Cliente? cliente = null)
    {
        InitializeComponent();
        Cliente = cliente ?? new Cliente();

        if (cliente != null)
        {
            TxtTitulo.Text = "Editar Cliente";
            TxtNomeCompleto.Text = Cliente.NomeCompleto;
            TxtCpf.Text = Cliente.Cpf;
            TxtRg.Text = Cliente.Rg;
            TxtCnh.Text = Cliente.Cnh;
            TxtCnhCategoria.Text = Cliente.CnhCategoria;
            TxtLogradouro.Text = Cliente.Logradouro;
            TxtNumero.Text = Cliente.Numero;
            TxtBairro.Text = Cliente.Bairro;
            TxtCep.Text = Cliente.Cep;
            TxtCidade.Text = Cliente.Cidade;
            TxtEstado.Text = Cliente.Estado;
            TxtTelefone.Text = Cliente.Telefone;
            TxtCelular.Text = Cliente.Celular;
        }
    }

    private void BtnSalvar_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtNomeCompleto.Text))
        {
            MessageBox.Show("Por favor, informe o nome completo do cliente.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
            TxtNomeCompleto.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(TxtCpf.Text))
        {
            MessageBox.Show("Por favor, informe o CPF do cliente.", "Validação", MessageBoxButton.OK, MessageBoxImage.Warning);
            TxtCpf.Focus();
            return;
        }

        Cliente.NomeCompleto = TxtNomeCompleto.Text.Trim();
        Cliente.Cpf = TxtCpf.Text.Trim();
        Cliente.Rg = TxtRg.Text.Trim();
        Cliente.Cnh = TxtCnh.Text.Trim();
        Cliente.CnhCategoria = TxtCnhCategoria.Text.Trim();
        Cliente.Logradouro = TxtLogradouro.Text.Trim();
        Cliente.Numero = TxtNumero.Text.Trim();
        Cliente.Bairro = TxtBairro.Text.Trim();
        Cliente.Cep = TxtCep.Text.Trim();
        Cliente.Cidade = TxtCidade.Text.Trim();
        Cliente.Estado = TxtEstado.Text.Trim();
        Cliente.Telefone = TxtTelefone.Text.Trim();
        Cliente.Celular = TxtCelular.Text.Trim();

        DialogResult = true;
        Close();
    }

    private void BtnCancelar_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
