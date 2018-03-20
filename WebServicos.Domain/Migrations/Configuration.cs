namespace WebServicos.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static WebServicos.Domain.Enum;

    internal sealed class Configuration : DbMigrationsConfiguration<WebServicos.Domain.ServicosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "WebServicos.Domain.ServicosContext";
        }

        protected override void Seed(WebServicos.Domain.ServicosContext servicosContext)
        {
            try
            {
                servicosContext.Database.Initialize(true);
                if (servicosContext.Cliente.Count() == 0)
                {
                    List<Cliente> clientes = new List<Cliente>()
                    {
                        new Cliente() { Id = 1, Nome = "Malwee", Bairro = "Barra do Rio Cerro", Cidade = "Jaragu� do Sul", Estado = "SC"},
                        new Cliente() { Id = 2, Nome = "Supero", Bairro = "Bucarein", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 3, Nome = "Avell", Bairro = "Centro", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 4, Nome = "Dell", Bairro = "Hortol�ndia", Cidade = "S�o Paulo", Estado = "SP"}

                    };
                    servicosContext.Cliente.AddRange(clientes);
                    servicosContext.SaveChanges();

                }
                if (servicosContext.Fornecedor.Count() == 0)
                {
                    List<Fornecedor> fornecedores = new List<Fornecedor>()
                    {
                        new Fornecedor() { Id = 1, Nome = "Jo�o", Email = "rafaeltwisted@gmail.com"},
                        new Fornecedor() { Id = 2, Nome = "Pedro", Email = "rafael.xavier@supero.com.br"},
                        new Fornecedor() { Id = 3, Nome = "Maria", Email = "rafael.asxavier@gmail.com"},
                        new Fornecedor() { Id = 4, Nome = "jos�", Email = "email@supero.com.br"}

                    };
                    servicosContext.Fornecedor.AddRange(fornecedores);
                    servicosContext.SaveChanges();
                }

                if (servicosContext.Servico.Count() == 0)
                {
                    List<Servico> servicos = new List<Servico>();
                    for (int i = 1; i <= 12; i++)
                    {
                        for (int x = 0; x < 10; x++)
                        {

                            Random random = new Random();
                            TipoServico tipoServico = (TipoServico)random.Next(1, 6);
                            var type = typeof(TipoServico);
                            var memInfo = type.GetMember(tipoServico.ToString());
                            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                            var description = ((DisplayAttribute)attributes[0]).Name;
                            servicos.Add(new Servico()
                            {
                                Cliente_ID = random.Next(1, 5),
                                Data = new DateTime(2018, i, random.Next(1, 28)),
                                Descricao = description,
                                Fornecedor_ID = random.Next(1, 5),
                                TipoServico = tipoServico,
                                Valor = random.Next(1, 1001)
                            });
                        }
                    }
                    servicosContext.Servico.AddRange(servicos);
                    servicosContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registros j� criados." + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
