using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;
namespace Shop.Query.SiteEntities.Sliders.GetList;

internal class GetSliderListQueryHandler : IQueryHandler<GetSliderListQuery, List<SliderDTO>>
{
    private readonly ShopContext _context;
    public GetSliderListQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<List<SliderDTO>> Handle(GetSliderListQuery request, CancellationToken cancellationToken)
    {
        var SliderList = await _context.Sliders.OrderByDescending(a => a.Id).Select(b => new SliderDTO
        {
            CreationDate = b.CreationDate,
            Id = b.Id,
            ImageName = b.ImageName,
            Link = b.Link,
            Title = b.Title,
        }).ToListAsync(cancellationToken);
        return SliderList;
    }
}
