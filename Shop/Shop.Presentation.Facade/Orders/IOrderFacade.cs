using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.CheckOut;
using Shop.Application.Orders.DecreaseCount;
using Shop.Application.Orders.IncreaseCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Query.Orders.DTOs;
namespace Shop.Presentation.Facade.Orders;
public interface IOrderFacade
{
    //Commands
    Task<OperationResult> AddOrderItem(AddOrderItemCommand command);
    Task<OperationResult> OrderCheckout(ChechOutOrderCommand command);
    Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command);
    Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command);
    Task<OperationResult> RemoveOrderItem(RemoveOrdertemCommand command);

    //Queries
    Task<OrderDto?> GetOrderById(long orderId);
    Task<OrderDto?> GetCurrentOrder(long userId);
    Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams);
}