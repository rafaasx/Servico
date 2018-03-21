using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServicos.Domain.Migrations
{
    public class DBInitializer : CreateDatabaseIfNotExists<ServicosContext>
    {
        private const int count = 20;

        protected override void Seed(ServicosContext servicosContext)
        {
            try
            {
                if (!servicosContext.Cliente.Any())
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
                if (!servicosContext.Fornecedor.Any())
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

                if (!servicosContext.Servico.Any())
                {
                    List<Servico> servicos = new List<Servico>();
                    for (int i = 1; i <= DateTime.Now.Month; i++)
                    {
                        for (int x = 0; x < count; x++)
                        {

                            Enum.TipoServico tipoServico = (Enum.TipoServico)new Random(DateTime.Now.Millisecond * x * i).Next(1, 6);
                            var type = typeof(Enum.TipoServico);
                            var memInfo = type.GetMember(tipoServico.ToString());
                            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                            var description = ((DisplayAttribute)attributes[0]).Name;
                            servicos.Add(new Servico()
                            {
                                Cliente_ID = new Random(DateTime.Now.Millisecond * x * i).Next(1, 5),
                                Data = new DateTime(2018, i, new Random(DateTime.Now.Millisecond * x * i).Next(1, 28)),
                                Descricao = description,
                                Fornecedor_ID = new Random(DateTime.Now.Millisecond * x * i).Next(1, 4),
                                TipoServico = tipoServico,
                                Valor = new Random(DateTime.Now.Millisecond * x * i).Next(1, 1001)
                            });
                        }
                    }
                    servicosContext.Servico.AddRange(servicos);
                    servicosContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registros já criados." + ex.Message + Environment.NewLine + ex.StackTrace);
            }

            base.Seed(servicosContext);
        }
    }
}
