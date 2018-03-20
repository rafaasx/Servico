using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicos.Domain;

namespace WebServicosTest.Domain
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class FornecedorTests
    {
        public FornecedorTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CriarFornecedorNulo()
        {
            FornecedorTests fornecedor = null;
            Assert.IsNull(fornecedor);
        }

        [TestMethod]
        public void CriarFornecedorSemNome()
        {
            Fornecedor fornecedor = new Fornecedor()
            {
                Id = 1,
                Email = "email@email.com.br"
            };

            Assert.IsNotNull(fornecedor.Email);
            Assert.IsNotNull(fornecedor.Id);
            Assert.IsNull(fornecedor.Nome);
        }

        [TestMethod]
        public void CriarFornecedorSemEmail()
        {
            Fornecedor fornecedor = new Fornecedor()
            {
                Id = 1,
                Nome = "Nome"
            };

            Assert.IsNull(fornecedor.Email);
            Assert.IsNotNull(fornecedor.Id);
            Assert.IsNotNull(fornecedor.Nome);
        }

        [TestMethod]
        public void CriarFornecedorSemId()
        {
            Fornecedor fornecedor = new Fornecedor()
            {
                Email = "email@email.com.br",
                Nome = "Nome"
            };

            Assert.IsNotNull(fornecedor.Email);
            Assert.AreEqual(0, fornecedor.Id);
            Assert.IsNotNull(fornecedor.Nome);
        }
    }
}
