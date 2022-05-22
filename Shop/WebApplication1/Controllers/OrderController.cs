using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.CheckOut;
using Shop.Application.Orders.DecreaseCount;
using Shop.Application.Orders.IncreaseCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Presentation.Facade.Orders;
using Shop.Query.Orders.DTOs;

namespace Shop.Api.Controllers;

public class OrderController : ApiController
{
    private readonly IOrderFacade _facade;
    public OrderController(IOrderFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<ApiResult<OrderFilterResult>> GetOrderByFilter([FromQuery] OrderFilterParams filterParams)
    {
        var result = await _facade.GetOrdersByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{orderId}")]
    public async Task<ApiResult<OrderDto?>> GetOrderById(long orderId)
    {
        var result = await _facade.GetOrderById(orderId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await _facade.AddOrderItem(command);
        return CommandResult(result);
    }

    [HttpPost("Checkout")]
    public async Task<ApiResult> CheckOutOrder(ChechOutOrderCommand command)
    {
        var result = await _facade.OrderCheckout(command);
        return CommandResult(result);
    }

    [HttpDelete("OrderItem")]
    public async Task<ApiResult> RemoveItem(RemoveOrdertemCommand command)
    {
        var result = await _facade.RemoveOrderItem(command);
        return CommandResult(result);
    }

    [HttpPut("IncreaseOrderItemCount")]
    public async Task<ApiResult> IncreaseOrderItemCount(IncreaseOrderItemCountCommand command)
    {
        var result = await _facade.IncreaseItemCount(command);
        return CommandResult(result);
    }

    [HttpPut("DecreaseOrderItemCount")]
    public async Task<ApiResult> DecreaseOrderItemCount(DecreaseOrderItemCountCommand command)
    {
        var result = await _facade.DecreaseItemCount(command);
        return CommandResult(result);
    }
}
