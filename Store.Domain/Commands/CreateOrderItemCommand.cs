using Store.Domain.Commands.Interfaces;
using Flunt.Validations;
using Flunt.Notifications;

namespace Store.Domain.Commands;
public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderItemCommand() { }

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Guid Product { get; private set; }
    public int Quantity { get; private set; }

    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterThan(Product.ToString(), 32, "Product", "Produto inválido")
            .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida"));
    }
}