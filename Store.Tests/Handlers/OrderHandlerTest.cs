using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTest
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTest()
        {
            _customerRepository = new CustomerRepositoryMock();
            _deliveryFeeRepository = new DeliveryFeeMock();
            _discountRepository = new DiscountRepositoryMock();
            _orderRepository = new OrderRepositoryMock();
            _productRepository = new ProductRepositoryMock();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            // Cria um command
            var command = createCommand("12345678912", "12345678", "123456", new CreateOrderItemCommand(Guid.NewGuid(), 1));

            // Instancia um handler e resolve as dependencias
            var handler = createHandler();

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmCepInvalidoOPedidoDeveSerGeradoNormalmente()
        {
            // Cria um command
            var command = createCommand("12345678911", "11111111", "123456", new CreateOrderItemCommand(Guid.NewGuid(), 1));

            // Instancia um handler e resolve as dependencias
            var handler = createHandler();

            handler.Handle(command);
            Assert.AreEqual(true, handler.Valid);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPromoCodeInvalidoOPedidoDeveSerGeradoNormalmente()
        {
            // Cria um command
            var command = createCommand("12345678911", "12345678", "11111111", new CreateOrderItemCommand(Guid.NewGuid(), 1));

            // Instancia um handler e resolve as dependencias
            var handler = createHandler();

            handler.Handle(command);
            Assert.AreEqual(true, handler.Valid);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
        {
            // Cria um command
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.Zipcode = "12345678";
            command.PromoCode = "12333";

            command.Validate();
            Assert.AreEqual(false, command.Valid);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.Zipcode = "12345678";
            command.PromoCode = "12333";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            // Cria um command
            var command = new CreateOrderCommand();
            command.Customer = "12345678";
            command.Zipcode = "12345678";
            command.PromoCode = "12333";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            // Instancia um handler e resolve as dependencias
            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository
            );

            handler.Handle(command);
            Assert.AreEqual(true, handler.Valid);
        }

        public CreateOrderCommand createCommand(string customer, string zipcode, string promocode, CreateOrderItemCommand itemCommand)
        {
            // Cria um command
            var command = new CreateOrderCommand();
            command.Customer = customer;
            command.Zipcode = zipcode;
            command.PromoCode = promocode;

            if (itemCommand != null)
                command.Items.Add(itemCommand);

            return command;
        }

        public OrderHandler createHandler()
        {
            return new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository
            );
        }
    }
}