using System;
using System.Linq;
using ClientManager.Models;

namespace ClientManager.Data;

public static class DbInitializer
{
    public static void Initialize()
    {
        using var context = new AppDbContext();
        context.Database.EnsureCreated();

        // Seed initial sample client if empty
        if (!context.Clientes.Any())
        {
            context.Clientes.Add(new Cliente
            {
                NomeCompleto = "Empresa Cliente Modelo LTDA",
                Cpf = "123.456.789-00",
                Rg = "12.345.678-9",
                Cnh = "12345678900",
                CnhCategoria = "B",
                Logradouro = "Av. Paulista",
                Numero = "1000",
                Bairro = "Bela Vista",
                Cidade = "São Paulo",
                Estado = "SP",
                Cep = "01310-100",
                Telefone = "(11) 3333-4444",
                Celular = "(11) 98888-7777",
                DataCadastro = DateTime.Now
            });
            context.SaveChanges();
        }
    }
}
