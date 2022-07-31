using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.SiteEntities.ShippingMethods.DTOs;

namespace Shop.Query.SiteEntities.ShippingMethods.GetById;

public class GetShippingMethodByIdQueryHandler : IQueryHandler<GetShippingMethodByIdQuery, ShippingMethodDTO?>
{
    private readonly DapperContext _context;
    public GetShippingMethodByIdQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<ShippingMethodDTO?> Handle(GetShippingMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select top(1) from {_context.ShippingMethod} where Id=@SHId";
        using var connection = _context.CreateConnection();
        var model = await connection.QueryFirstOrDefaultAsync<ShippingMethodDTO?>(sql, new { SHMId = request.Id });
        return model;
    }
}
