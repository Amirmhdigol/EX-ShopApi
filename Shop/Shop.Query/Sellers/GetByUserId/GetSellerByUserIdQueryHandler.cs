using Microsoft.EntityFrameworkCore;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers.GetByUserId;

public class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDTO>
{
    private readonly ShopContext _context;
    public GetSellerByUserIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDTO> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Sellers.FirstOrDefaultAsync(a => a.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        return user.Map();
    }
}