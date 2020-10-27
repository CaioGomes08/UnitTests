using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
    public class DeliveryFeeMock : IDeliveryFeeRepository
    {
        public decimal Get(string zipCode)
        {
            return 10;
        }
    }
}