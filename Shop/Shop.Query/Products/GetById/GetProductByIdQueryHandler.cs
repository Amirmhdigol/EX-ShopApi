using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
namespace Shop.Query.Products.GetById;
public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDTO?>
{
    private readonly ShopContext _context;
    public GetProductByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == request.ProductId, cancellationToken);
        var model = product.Map();
        if (model == null) return null;
        await model.SetCategories(_context);
        return model;
    }
}