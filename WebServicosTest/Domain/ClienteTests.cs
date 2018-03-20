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
    public class ClienteTests
    {
        public ClienteTests()
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
        public void CriarClienteNulo()
        {
            ClienteTests Cliente = null;
            Assert.IsNull(Cliente);
        }

        [TestMethod]
        public void CriarClienteSemNome()
        {
            Cliente Cliente = new Cliente()
            {
                Id = 1,
                Bairro = "Bairro",
                Cidade = "Cidade",
                Estado = "Estado",
                Nome = null
            };

            Assert.IsNotNull(Cliente.Bairro);
            Assert.IsNotNull(Cliente.Id);
            Assert.IsNotNull(Cliente.Cidade);
            Assert.IsNotNull(Cliente.Estado);
            Assert.IsNull(Cliente.Nome);
        }
    }
}
