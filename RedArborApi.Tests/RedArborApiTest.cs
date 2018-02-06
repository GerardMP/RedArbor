using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedArbor.Data.DAO;
using RedArbor.Data.Entities;
using RedArbor.WebApi.Controllers;

namespace RedArbor.WebApi.Tests
{
    /// <summary>
    /// Vlase de test vacía 
    /// </summary>
    [TestClass]
    public class RedArborTest
    {
        private const string _connectionString = "Data Source=.;Initial Catalog=RedArbor;Integrated Security=True;Trusted_Connection=True;Connect Timeout=30;TrustServerCertificate=True";
        private RedArborController controller;

        public RedArborTest()
        {
            var dao = new EmployeeDAO(_connectionString);
            controller = new RedArborController(dao);
        }

        #region Api individual methods
        
        [TestMethod]
        public void Get()
        {
            var result = controller.Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Employee[]));
        }

        [TestMethod]
        public void GetById()
        {
            //string result = controller.Get(5);
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        { 
            //controller.Post("value");
        }

        [TestMethod]
        public void Put()
        {
            //controller.Put(5, "value");
        }

        [TestMethod]
        public void Delete()
        {
            //controller.Delete(5);
        }

        #endregion

        #region Data integrity Tests

        [TestMethod]
        public void LifeCicle()
        {
            // Create 

            // Get

            // GetAll

            // Update

            // Get

            // Delete

            // GetAll

            // Check
        }

        #endregion

        #region Concurrency and performance Tests

        // Posible ampliación en el test RedArbor

        #endregion
    }
}
