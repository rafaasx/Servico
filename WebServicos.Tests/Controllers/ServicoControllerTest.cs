using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebServicos.Controllers;
using WebServicos.Models;

namespace WebServicos.Tests.Controllers
{
    [TestClass]
    public class ServicoControllerTest
    {
        private ServicosContext _servicosContext;

        public ServicoControllerTest()
        {
            _servicosContext = new ServicosContext();
            try
            {
                _servicosContext.Database.Initialize(true);
                if (_servicosContext.Cliente.Count() == 0)
                {
                    List<Cliente> clientes = new List<Cliente>()
                    {
                        new Cliente() { Id = 1, Nome = "Malwee", Bairro = "Barra do Rio Cerro", Cidade = "Jaraguá do Sul", Estado = "SC"},
                        new Cliente() { Id = 2, Nome = "Supero", Bairro = "Bucarein", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 3, Nome = "Avell", Bairro = "Centro", Cidade = "Joinville", Estado = "SC"},
                        new Cliente() { Id = 4, Nome = "Dell", Bairro = "Hortolândia", Cidade = "São Paulo", Estado = "SP"}

                    };
                    _servicosContext.Cliente.AddRange(clientes);
                    _servicosContext.SaveChanges();

                }
                if (_servicosContext.Fornecedor.Count() == 0)
                {
                    List<Fornecedor> fornecedores = new List<Fornecedor>()
                    {
                        new Fornecedor() { Id = 1, Nome = "João", Email = "rafaeltwisted@gmail.com"},
                        new Fornecedor() { Id = 2, Nome = "Pedro", Email = "rafael.xavier@supero.com.br"},
                        new Fornecedor() { Id = 3, Nome = "Maria", Email = "rafael.asxavier@gmail.com"},
                        new Fornecedor() { Id = 4, Nome = "josé", Email = "email@supero.com.br"}

                    };
                    _servicosContext.Fornecedor.AddRange(fornecedores);
                    _servicosContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registros já criados." + ex.Message + "\n" + ex.StackTrace);
            }
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            ServicoController controller = new ServicoController();

            // Act
            Task<ActionResult> result = controller.Index() as Task<ActionResult>;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateService()
        {
            Servico servico = new Servico()
            {
                Cliente_ID = 1,
                Data = new DateTime(2018, 3, 12),
                Descricao = "Jardinagem",
                TipoServico = Models.Enum.TipoServico.Jardinagem,
                Valor = 100,
                Fornecedor_ID = _servicosContext.Fornecedor.First().Id
            };

            ServicoController servicoController = new ServicoController();
            ActionResult result = servicoController.Create(servico) as ActionResult;
            //Servico servicoResult = _servicosContext.Servico.First(x => x.Id == result.Result.);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, servico.Id);
            //Assert.AreEqual(servico, servicoResult);
            //_servicosContext.Servico.Remove(servicoResult);
        }
    }
}
