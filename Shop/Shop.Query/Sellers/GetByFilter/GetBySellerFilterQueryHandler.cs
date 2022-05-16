using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter;

internal class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;
    public GetSellerByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var parameters = request.FilterParams;
        var result = _context.Sellers.OrderByDescending(a => a.Id).AsQueryable();

        if (string.IsNullOrWhiteSpace(parameters.NationalCode))
            result = result.Where(a => a.NationalCode.Contains(parameters.NationalCode));

        if (string.IsNullOrWhiteSpace(parameters.ShopName))
            result = result.Where(a => a.ShopName.Contains(parameters.ShopName));

        var skip = (parameters.PageId - 1) * parameters.Take;
        var sellerResult = new SellerFilterResult()
        {
            FilterParams = parameters,
            Data = await result.Skip(skip).Take(parameters.Take).Select(seller => seller.Map()).ToListAsync(cancellationToken)
        };
        sellerResult.GeneratePaging(result, parameters.Take, parameters.PageId);
        return sellerResult;
    }
}