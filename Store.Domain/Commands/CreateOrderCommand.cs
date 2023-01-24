using Store.Domain.Commands.Interfaces;
using Flunt.Validations;
using Flunt.Notifications;

namespace Store.Domain.Commands;
public class CreateOrderCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderCommand()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommand(
        string customer, 
        string zipCode, 
        string promoCode, 
        IList<CreateOrderItemCommand> items)
    {
        Customer = customer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }

    public string Customer { get; private set; }
    public string ZipCode { get; private set; }
    public string PromoCode { get; private set; }
    public IList<CreateOrderItemCommand> Items { get; private set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterThan(Customer, 11, "Customer", "Cliente inválido")
            .IsGreaterThan(ZipCode, 8, "ZipCode", "CEP inválido"));
    }
}