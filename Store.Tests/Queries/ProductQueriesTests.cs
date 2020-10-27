using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private IList<Product> _products;

        public ProductQueriesTests()
        {
            _products = new List<Product>();
            _products.Add(new Product("Produto 1", 10, true));
            _products.Add(new Product("Produto 2", 20, true));
            _products.Add(new Product("Produto 3", 30, true));
            _products.Add(new Product("Produto 4", 40, false));
            _products.Add(new Product("Produto 5", 50, false));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void DadoAConsultaDeProdutosAtivosDeveRetornar3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void DadoAConsultaDeProdutosInativosDeveRetornar2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(2, result.Count());
        }


    }
}