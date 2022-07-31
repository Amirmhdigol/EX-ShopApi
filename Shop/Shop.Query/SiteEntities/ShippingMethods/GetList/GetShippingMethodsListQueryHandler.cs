using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.SiteEntities.ShippingMethods.DTOs;

namespace Shop.Query.SiteEntities.ShippingMethods.GetList;

public class GetShippingMethodsListQueryHandler : IQueryHandler<GetShippingMethodsListQuery, List<ShippingMethodDTO>>
{
    private readonly DapperContext _context;
    public GetShippingMethodsListQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<ShippingMethodDTO>> Handle(GetShippingMethodsListQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select * from {_context.ShippingMethod}";
        using var connection = _context.CreateConnection();
        var model = await connection.QueryAsync<ShippingMethodDTO?>(sql);
        return model.ToList();
    }
}
