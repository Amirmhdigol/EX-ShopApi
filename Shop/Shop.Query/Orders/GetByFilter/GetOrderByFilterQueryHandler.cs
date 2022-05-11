using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.GetByFilter
{
    internal class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
    {
        private readonly ShopContext _context;
        public GetOrderByFilterQueryHandler(ShopContext context)
        {
            _context = context;
        }
        public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;
            var result = _context.Orders.OrderByDescending(a => a.Id).AsQueryable();

            if (@params.Status != null)
                result = result.Where(a => a.Status == @params.Status);

            if (@params.UserId != null)
                result = result.Where(a => a.UserId == @params.UserId);

            if (@params.StartDate != null)
                result = result.Where(a => a.CreationDate.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate != null)
                result = result.Where(a => a.CreationDate.Date <= @params.EndDate.Value.Date);

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new OrderFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(a => a.MapFilterData(_context)).ToListAsync(cancellationToken),
                FilterParams = @params,
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
