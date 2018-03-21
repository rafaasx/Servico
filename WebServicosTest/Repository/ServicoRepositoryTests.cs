using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServicos.Domain;
using System.Data.Entity.Validation;
using Enum = WebServicos.Domain.Enum;

namespace WebServicos.Repository.Tests
{
    [TestClass()]
    public class ServicoRepositoryTests
    {
        [ClassInitialize()]
        public static void Initialize(TestContext testContext) {
            new ServicoRepository().CleanUp();
        }


        [TestMethod()]
        public void ServicoRepositoryAddTest()
        {
            Servico servico = new Servico()
            {
                Cliente_ID = 1,
                Fornecedor_ID = 1,
                Data = DateTime.Now,
                Descricao = "Manutenção",
                TipoServico = Enum.TipoServico.ServicosGerais,
                Valor = 100
            };
            ServicoRepository servicoRepository = new ServicoRepository();
            servicoRepository.Add(servico);
            Assert.IsNotNull(servico.Id);
            servicoRepository.Delete(servico.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(DbEntityValidationException),
    "O campo Nome é obrigatório.")]
        public void ServicoRepositoryAddWithoutNameTest()
        {
            Servico servico = new Servico()
            {
                Cliente_ID = 1,
                Fornecedor_ID = 1,
                Data = DateTime.Now,
                Descricao = "Manutenção",
                TipoServico = Enum.TipoServico.ServicosGerais,
                Valor = 100
            };
            ServicoRepository servicoRepository = new ServicoRepository();
            servicoRepository.Add(servico);
        }

        [ExpectedException(typeof(DbEntityValidationException),
"Não foi possível encontrar o fornecedor com código {0}")]
        [TestMethod()]
        public void FornecedorRepositoryDeleteTest()
        {
            new FornecedorRepository().Delete(99999);
        }

        [TestMethod()]
        public void FornecedorRepositoryGetTest()
        {
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            Fornecedor fornecedor = new Fornecedor() { Nome = "Nome", Email = "email@email.com" };
            fornecedorRepository.Add(fornecedor);
            Fornecedor fornecedorResult = fornecedorRepository.Get(fornecedor.Id);
            Assert.AreEqual(fornecedor.Id, fornecedorResult.Id);
            Assert.AreEqual(fornecedor.Nome, fornecedorResult.Nome);
            Assert.AreEqual(fornecedor.Email, fornecedorResult.Email);
            fornecedorRepository.Delete(fornecedor.Id);
        }

        [TestMethod()]
        public void FornecedorRepositoryGetNotFoundTest()
        {
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            Fornecedor fornecedor = new Fornecedor() { Nome = "Nome", Email = "email@email.com" };
            fornecedorRepository.Add(fornecedor);
            Fornecedor fornecedorResult = fornecedorRepository.Get(-1);
            Assert.IsNull(fornecedorResult);
            fornecedorRepository.Delete(fornecedor.Id);
        }

        [TestMethod()]
        public void FornecedorRepositoryListTest()
        {
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            Fornecedor fornecedorA = new Fornecedor() { Nome = "NomeA", Email = "email@email.com" };
            Fornecedor fornecedorB = new Fornecedor() { Nome = "NomeB", Email = "email@email.com" };
            fornecedorRepository.Add(fornecedorA);
            fornecedorRepository.Add(fornecedorB);
            var fornecedores = fornecedorRepository.List();
            Assert.AreEqual(fornecedores.Count(), 2);
            Assert.AreEqual(fornecedores[0].Id, fornecedorA.Id);
            Assert.AreEqual(fornecedores[1].Id, fornecedorB.Id);
            fornecedorRepository.Delete(fornecedorA.Id);
            fornecedorRepository.Delete(fornecedorB.Id);
        }

        [TestMethod()]
        public void FornecedorRepositoryUpdateTest()
        {
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            Fornecedor fornecedor = new Fornecedor() { Nome = "Nome", Email = "email@email.com" };
            fornecedorRepository.Add(fornecedor);
            Fornecedor fornecedorNew = new Fornecedor() { Id = fornecedor.Id, Nome = "Nome 2", Email = "email2@email2.com" };
            fornecedorRepository.Update(fornecedorNew);
            fornecedor = fornecedorRepository.Get(fornecedor.Id);
            Assert.AreEqual(fornecedor.Id, fornecedorNew.Id);
            Assert.AreEqual(fornecedor.Nome, fornecedorNew.Nome);
            Assert.AreEqual(fornecedor.Email, fornecedorNew.Email);
            fornecedorRepository.Delete(fornecedor.Id);
        }
    }
}