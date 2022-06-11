using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;
public record GetCurrentOrderQuery(long UserId) : IQuery<OrderDto>;
