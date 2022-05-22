using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.GetByParentId
{
    internal class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery, List<CategoryChildDto>>
    {
        private readonly ShopContext _context;
        public GetCategoryByParentIdQueryHandler(ShopContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryChildDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories.Include(a=>a.Childs)
                .Where(a => a.ParentId == request.ParentId).ToListAsync(cancellationToken);
            return result.MapChildren();
        }
    }
}
