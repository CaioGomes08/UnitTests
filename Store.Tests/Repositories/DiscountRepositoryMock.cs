using System;
using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class DiscountRepositoryMock : IDiscountRepository
    {
        public Discount Get(string code)
        {
            if (code == "12345678")
                return new Discount(10, DateTime.Now.AddDays(2));

            if (code == "11111111")
                return new Discount(10, DateTime.Now.AddDays(-2));

            return null;
        }
    }
}