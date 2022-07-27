using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

internal class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Comments.OrderByDescending(a => a.CreationDate).AsQueryable();

        if (@params.ProductId != null)
            result = result.Where(r => r.UserId == @params.ProductId);

        if (@params.CommentStatus != null)
            result = result.Where(a => a.Status == @params.CommentStatus);

        if (@params.UserId != null)
            result = result.Where(a => a.UserId == @params.UserId);

        if (@params.StartDate != null)
            result = result.Where(a => a.CreationDate.Date >= @params.StartDate.Value.Date);

        if (@params.EndDate != null)
            result = result.Where(a => a.CreationDate.Date <= @params.EndDate.Value.Date);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new CommentFilterResult()
        {
            Data = await result.Skip(skip).Take(@params.Take).Select(a => a.MapFilterComment()).ToListAsync(cancellationToken),
            FilterParams = @params,
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}

