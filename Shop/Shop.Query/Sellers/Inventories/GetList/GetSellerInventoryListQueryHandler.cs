using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers.Inventories.GetList;

internal class GetSellerInventoryListQueryHandler : IQueryHandler<GetSellerInventoryListQuery, List<InventoryDTO>>
{
    private readonly DapperContext _context;
    public GetSellerInventoryListQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryDTO>> Handle(GetSellerInventoryListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();

        var sql = @$"SELECT i.Id, i.SellerId , i.ProductId ,i.Count , i.Price,i.CreationDate , i.DiscountPercentage , s.ShopName ,
                        p.Title as ProductTitle,p.ImageName as ProductImage
            FROM {_context.Inventories} i inner join {_context.Sellers} s on i.SellerId=s.Id  
            inner join {_context.Products} p on i.ProductId=p.Id WHERE i.SellerId=@sellerId";

        var inventories = await connection.QueryAsync<InventoryDTO>(sql, new { sellerId = request.SellerId });
        return inventories.ToList();
    }
}