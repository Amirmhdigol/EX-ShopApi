using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;
namespace Shop.Query.SiteEntities.Banners.GetById;

internal class GetBannerByIdQueryHandler : IQueryHandler<GetBannerByIdQuery, BannerDTO>
{
    private readonly ShopContext _context;
    public GetBannerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<BannerDTO> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Banners.FirstOrDefaultAsync(a => a.Id == request.BannerId);
        if (model == null) return null;
        return new BannerDTO()
        {
            CreationDate = model.CreationDate,
            Id = model.Id,
            ImageName = model.ImageName,
            Link = model.Link,
            Position = model.Position,
        };
    }
}
