using Store.Tests.Repositories;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Handlers;
[TestClass]
public class OrtderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrtderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _orderRepository = new FakeOrderRepository();
        _productRepository = new FakeProductRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldNotGenerateAnOrderWhenCommandIsInvalid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "";
        command.ZipCode = "13411080";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Validate();

        Assert.AreEqual(command.IsValid, false);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void ShouldGenerateAnOrderWhenCommandIsValid()
    {
        var command = new CreateOrderCommand();
        command.Customer = "12345678";
        command.ZipCode = "13411080";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        var handler = new OrderHandler(
            _customerRepository,
            _deliveryFeeRepository,
            _discountRepository,
            _productRepository,
            _orderRepository);

        handler.Handle(command);
        Assert.AreEqual(handler.IsValid, true);
    }
}