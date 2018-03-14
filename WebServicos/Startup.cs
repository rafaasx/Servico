using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using WebServicos.Domain;

[assembly: OwinStartupAttribute(typeof(WebServicos.Startup))]
namespace WebServicos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            try
            {
                ServicosContext servicosContext = new ServicosContext();
                servicosContext.Database.Initialize(true);
                if (servicosContext.Cliente.Count() == 0)
                {
                    List<Cliente> clientes = new List<Cliente>()
                    {
                        new Cliente() { Id = 1, Nome = "Malwee", Bairro = "Barra do Rio Cerro", Cidade = "Jaraguá do Sul", Estado = "SC"},
                        new Cliente() { Id = 2, Nome = "Supero", Bairro = "Bucarein", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 3, Nome = "Avell", Bairro = "Centro", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 4, Nome = "Dell", Bairro = "Hortolândia", Cidade = "São Paulo", Estado = "SP"}

                    };
                    servicosContext.Cliente.AddRange(clientes);
                    servicosContext.SaveChanges();

                }
                if (servicosContext.Fornecedor.Count() == 0)
                {
                    List<Fornecedor> fornecedores = new List<Fornecedor>()
                    {
                        new Fornecedor() { Id = 1, Nome = "João", Email = "rafaeltwisted@gmail.com"},
                        new Fornecedor() { Id = 2, Nome = "Pedro", Email = "rafael.xavier@supero.com.br"},
                        new Fornecedor() { Id = 3, Nome = "Maria", Email = "rafael.asxavier@gmail.com"},
                        new Fornecedor() { Id = 4, Nome = "josé", Email = "email@supero.com.br"}

                    };
                    servicosContext.Fornecedor.AddRange(fornecedores);
                    servicosContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registros já criados." + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
