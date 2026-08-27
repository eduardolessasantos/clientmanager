using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientManager.Data;
using ClientManager.Models;
using ClientManager.Services;
using Microsoft.Win32;

namespace ClientManager.Views;

public partial class MainWindow : Window
{
    private List<Cliente> _todosClientes = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        InicializarBancoECarregar();
    }

    private void InicializarBancoECarregar()
    {
        try
        {
            DbInitializer.Initialize();
            CarregarClientes();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao conectar no banco ({ConfigService.ModoAtual}): {ex.Message}", "Erro de Conexão", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CarregarClientes()
    {
        using var context = new AppDbContext();
        _todosClientes = context.Clientes.OrderByDescending(c => c.DataCadastro).ToList();
        FiltrarEAtualizarGrid();
    }

    private void FiltrarEAtualizarGrid()
    {
        var termo = TxtBusca?.Text?.Trim() ?? "";

        var clientesFiltrados = string.IsNullOrWhiteSpace(termo)
            ? _todosClientes
            : _todosClientes.Where(c => (c.NomeCompleto != null && c.NomeCompleto.Contains(termo, StringComparison.OrdinalIgnoreCase)) ||
                                        (c.Cpf != null && c.Cpf.Contains(termo, StringComparison.OrdinalIgnoreCase))).ToList();

        GridClientes.ItemsSource = clientesFiltrados;
        TxtTotal.Text = $"Total de clientes: {_todosClientes.Count} | Conectado em: {ConfigService.ModoAtual}";
    }

    private void TxtBusca_TextChanged(object sender, TextChangedEventArgs e)
    {
        FiltrarEAtualizarGrid();
    }

    private void BtnLimparBusca_Click(object sender, RoutedEventArgs e)
    {
        TxtBusca.Text = string.Empty;
    }

    private void BtnNovo_Click(object sender, RoutedEventArgs e)
    {
        var window = new ClienteFormWindow { Owner = this };
        if (window.ShowDialog() == true)
        {
            using var context = new AppDbContext();
            context.Clientes.Add(window.Cliente);
            context.SaveChanges();
            CarregarClientes();
            TxtStatus.Text = $"Cliente '{window.Cliente.NomeCompleto}' cadastrado com sucesso.";
        }
    }

    private void BtnEditar_Click(object sender, RoutedEventArgs e)
    {
        EditarSelecionado();
    }

    private void GridClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        EditarSelecionado();
    }

    private void EditarSelecionado()
    {
        if (GridClientes.SelectedItem is not Cliente selecionado)
        {
            MessageBox.Show("Selecione um cliente na lista para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var window = new ClienteFormWindow(selecionado) { Owner = this };
        if (window.ShowDialog() == true)
        {
            using var context = new AppDbContext();
            context.Clientes.Update(window.Cliente);
            context.SaveChanges();
            CarregarClientes();
            TxtStatus.Text = $"Cliente '{window.Cliente.NomeCompleto}' atualizado com sucesso.";
        }
    }

    private void BtnExcluir_Click(object sender, RoutedEventArgs e)
    {
        if (GridClientes.SelectedItem is not Cliente selecionado)
        {
            MessageBox.Show("Selecione um cliente na lista para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        var confirm = MessageBox.Show($"Deseja realmente excluir o cliente '{selecionado.NomeCompleto}'?", "Confirmação de Exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (confirm == MessageBoxResult.Yes)
        {
            using var context = new AppDbContext();
            context.Clientes.Remove(selecionado);
            context.SaveChanges();
            CarregarClientes();
            TxtStatus.Text = $"Cliente '{selecionado.NomeCompleto}' excluído com sucesso.";
        }
    }

    private async void BtnImportar_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Arquivos CSV / Texto (*.csv;*.txt)|*.csv;*.txt|Todos os Arquivos (*.*)|*.*",
            Title = "Selecionar arquivo da base de dados antiga (~9000 clientes)"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            ProgressoImportacao.Visibility = Visibility.Visible;
            ProgressoImportacao.Value = 0;
            TxtStatus.Text = "Iniciando importação da base de dados antiga...";
            BtnImportar.IsEnabled = false;

            var progressHandler = new Progress<int>(value =>
            {
                ProgressoImportacao.Value = value;
                TxtStatus.Text = $"Importando base antiga... {value}%";
            });

            var importService = new ImportService();
            var resultado = await importService.ImportarDeCsvAsync(openFileDialog.FileName, progressHandler);

            ProgressoImportacao.Visibility = Visibility.Collapsed;
            BtnImportar.IsEnabled = true;

            CarregarClientes();

            var mensagem = $"Importação Concluída!\n\nClientes importados com sucesso: {resultado.SucessoCount}\nErros/Ignorados: {resultado.ErrosCount}";
            MessageBox.Show(mensagem, "Resultado da Migração", MessageBoxButton.OK, MessageBoxImage.Information);
            TxtStatus.Text = $"Importação finalizada. {resultado.SucessoCount} registros migrados.";
        }
    }

    private void CboModoBanco_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded) return;

        if (CboModoBanco.SelectedItem is ComboBoxItem item && item.Tag is string modo)
        {
            ConfigService.DefinirModo(modo);
            InicializarBancoECarregar();
        }
    }
}
