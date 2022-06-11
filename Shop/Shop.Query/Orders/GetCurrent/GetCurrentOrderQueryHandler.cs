using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetCurrent;

public class GetCurrentOrderQueryHandler : IQueryHandler<GetCurrentOrderQuery, OrderDto>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;
    public GetCurrentOrderQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetCurrentOrderQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Orders.FirstOrDefaultAsync(a => a.UserId == request.UserId && a.Status == OrderStatus.pennding, cancellationToken);
        if (user == null) return null;

        var orderDto = user.Map();
        orderDto.UserFullName = await _shopContext.Users.Where(f => f.Id == orderDto.UserId)
                   .Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}
