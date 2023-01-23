using Store.Domain.Enums;
using Store.Domain.Entities;

namespace Store.Tests.Entities;
// [TestClass]
public class OrderTests
{
    private readonly Customer _customer = new Customer("Andre Baltieri", "andre@balta.io");
    private readonly Product _product = new Product("Produto 1", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnAn8CharactersNumberIfOrderIsValid()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(8, order.Number.Length);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnWaitingPaymentIfOrderedAnItem()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnWaitingDeliveryIfTheItemWasPaid()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 1);
        order.Pay(10);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnCanceledIfTheOrderedWasCanceled()
    {
        var order = new Order(_customer, 0, null);
        order.Cancel();
        Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnIfOrderANewItemWithoutProduct()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(null, 1);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnIfOrderANewItemWithQuantityLessOrEqualsZero()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 0);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturn50IfNewOrderIsValid()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturn60IfDiscountExpired()
    {
        var order = new Order(_customer, 10, new Discount(10, DateTime.Now.AddDays(-5)));
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturn60IfDiscountIsInvalid()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturn50IfDiscountIsValid()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturn60IfDeliveryFeeIs10()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void ShouldReturnInvalidIfOrderAnItemWithoutACustomer()
    {
        var order = new Order(null, 10, _discount);
        Assert.AreEqual(order.IsValid, false);
    }
}