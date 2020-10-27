using System;
using System.Collections.Generic;
using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class ProductRepositoryMock : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("Produto 1", 10, true));
            products.Add(new Product("Produto 2", 10, true));
            products.Add(new Product("Produto 3", 10, true));
            products.Add(new Product("Produto 4", 10, true));
            products.Add(new Product("Produto 5", 10, true));

            return products;
        }
    }
}