using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers.GetById;

public class GetSellerByIdQueryHandler : IQueryHandler<GetSellerByIdQuery, SellerDTO?>
{
    private readonly ShopContext _context;
    public GetSellerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<SellerDTO?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Sellers.FirstOrDefaultAsync(a => a.Id == request.SellerId);

        return model.Map();
    }
}