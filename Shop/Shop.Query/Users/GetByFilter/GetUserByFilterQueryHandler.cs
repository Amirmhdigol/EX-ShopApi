using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter;

internal class GetUserByFilterQueryHandler : IQueryHandler<GetUserByFilterQuery, UserFilterResult>
{
    private readonly ShopContext _context;
    public GetUserByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUserByFilterQuery request, CancellationToken cancellationToken)
    {
        var parameters = request.FilterParams;
        var result = _context.Users.OrderByDescending(a => a.Id).AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.PhoneNumber))
            result = result.Where(a => a.PhoneNumber.Contains(parameters.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(parameters.Email))
            result = result.Where(a => a.Email.Contains(parameters.Email));

        if (parameters.Id != null)
            result = result.Where(a => a.Id == parameters.Id);

        var skip = (parameters.PageId - 1) * parameters.Take;
        var model = new UserFilterResult()
        {
            Data = await result.Skip(skip).Take(parameters.Take).Select(a => a.MapFilterData()).ToListAsync(cancellationToken),
            FilterParams = parameters,
        };
        model.GeneratePaging(result, parameters.Take, parameters.PageId);
        return model;
    }
}
