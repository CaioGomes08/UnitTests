using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class CustomerRepositoryMock : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "1234567811")
                return new Customer("Bruce Wayne", "batma@balta.io");

            return null;
        }
    }
}