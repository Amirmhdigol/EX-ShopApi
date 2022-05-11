using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetById;

internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;
    public GetOrderByIdQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == request.OrderId);

        if (order == null)
            return null;

        var orderDto = order.Map();

        orderDto.UserFullName = await _context.Users.Where(a => a.Id == orderDto.UserId)
            .Select(a => $"{a.Name} {a.Family}").FirstAsync(cancellationToken);

        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}

