using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders.GetById;

internal class GetSliderByIdQueryHandler : IQueryHandler<GetSliderByIdQuery, SliderDTO>
{
    private readonly ShopContext _context;
    public GetSliderByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<SliderDTO> Handle(GetSliderByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Sliders.FirstOrDefaultAsync(a => a.Id == request.SliderId,cancellationToken);
        if (model == null) return null;
        return new SliderDTO()
        {
            CreationDate = model.CreationDate,
            Id = model.Id,
            ImageName = model.ImageName,
            Link = model.Link,
            Title = model.Title,
        };
    }
}

