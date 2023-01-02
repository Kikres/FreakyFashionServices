using Order.Data.Models;
using Order.Data.Repository;
using Order.Processor.Client;
using Order.Processor.Models;

namespace Order.Processor.Service;

public class OrderService
{
    private readonly OrderRepositroy _orderRepositroy;
    private readonly CustomerRepositroy _customerRepositroy;
    private readonly BasketCllient _basketCllient;

    public OrderService()
    {
        _orderRepositroy = new OrderRepositroy();
        _customerRepositroy = new CustomerRepositroy();
        _basketCllient = new BasketCllient();
    }

    public async Task CreateOrder(NewOrderRequestDto newOrderDto)
    {
        var basketDto = await _basketCllient.GetBasketByIdentifier(newOrderDto.Identifier);
        if (basketDto == null) new ArgumentNullException("Could not load basket");

        var order = new Data.Models.Order() { Id = newOrderDto.Id };

        var existingCustomer = await _customerRepositroy.GetCustomerByEmail(newOrderDto.Email);
        if (existingCustomer == null)
        {
            order.Customer = new Customer() { FirstName = newOrderDto.FirstName, LastName = newOrderDto.LastName, Email = newOrderDto.Email };
        }
        else
        {
            order.CustomerId = existingCustomer.Id;
        }

        order.Products = basketDto.Items.Select(o => new OrderLine { Quantity = o.Quantity, Sku = o.Sku }).ToList();

        var createdOrder = await _orderRepositroy.CreateOrder(order);
        _basketCllient.DeleteBasketByIdentifier(newOrderDto.Identifier);
    }
}