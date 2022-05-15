using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;
namespace Shop.Query.SiteEntities.Banners.GetList;

internal class GetBannersListQueryHandler : IQueryHandler<GetBannersListQuery, List<BannerDTO>>
{
    private readonly ShopContext _context;
    public GetBannersListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<BannerDTO>> Handle(GetBannersListQuery request, CancellationToken cancellationToken)
    {
        var BannerList = await _context.Banners.OrderByDescending(a => a.Id).Select(b => new BannerDTO
        {
            CreationDate = b.CreationDate,
            Id = b.Id,
            ImageName = b.ImageName,
            Link = b.Link,
            Position = b.Position,
        }).ToListAsync(cancellationToken);
        return BannerList;
    }
}