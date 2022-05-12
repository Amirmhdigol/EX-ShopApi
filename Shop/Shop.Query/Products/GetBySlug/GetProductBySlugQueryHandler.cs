using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
namespace Shop.Query.Products.GetBySlug;

public class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDTO?>
{
    private readonly ShopContext _context;
    public GetProductBySlugQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductDTO?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.FirstOrDefaultAsync(a => a.Slug == request.Slug, cancellationToken);
        var model = products.Map();

        if (model == null) return null;
        await model.SetCategories(_context);
        return model;
    }
}
