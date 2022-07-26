using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetByProductId;

public class GetInventoriesByProductIdQueryHandler : IQueryHandler<GetInventoriesByProductIdQuery, List<InventoryDTO>>
{
    private readonly DapperContext _dapperContext;
    public GetInventoriesByProductIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<InventoryDTO>> Handle(GetInventoriesByProductIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"select si.SellerId,s.Id,si.ProductId,si.[Count],s.CreationDate,si.DiscountPercentage," +
            $"si.Price,p.Title,p.ImageName,s.ShopName from {_dapperContext.Inventories} si" +
            $" inner join {_dapperContext.Products} p on si.ProductId = p.Id" +
            $" inner join {_dapperContext.Sellers} s on si.SellerId = s.Id where ProductId=@PId";

        using var connection = _dapperContext.CreateConnection();
        var inventory = await connection.QueryAsync<InventoryDTO>(sql,new {PID = request.ProductId});
        return inventory.ToList();
    }
}