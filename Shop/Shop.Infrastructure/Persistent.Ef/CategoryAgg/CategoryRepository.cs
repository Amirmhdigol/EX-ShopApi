using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAgg;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Ef;

namespace Shop.Infrastructure.Persistent.Ef.CategoryAgg
{
    internal class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext context) : base(context)
        {
        }

        public async Task<bool> DeleteCategory(long categoryId)
        {
            var category = await _context.Categories.Include(a => a.Childs).ThenInclude(s => s.Childs)
                .FirstOrDefaultAsync(a => a.Id == categoryId);

            if (category == null) return false;

            var ContainsAnyProducts = await _context.Products.AnyAsync(a => a.CategoryId == categoryId || a.SubCategoryId == categoryId ||
                                                                         a.SecondrySubCategoryId == categoryId);

            if (ContainsAnyProducts) return false;

            if (category.Childs.Any(a => a.Childs.Any()))
                _context.RemoveRange(category.Childs.Select(a => a.Childs));

            _context.RemoveRange(category.Childs);
            _context.RemoveRange(category);
            return true;
        }
    }
}