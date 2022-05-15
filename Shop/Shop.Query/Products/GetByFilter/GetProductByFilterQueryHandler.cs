﻿using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
namespace Shop.Query.Products.GetByFilter;

public class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _context;
    public GetProductByFilterQueryHandler(ShopContext shopContext)
    {
        _context = shopContext;
    }
    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var result = _context.Products.OrderByDescending(a => a.CreationDate).AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Slug))
            result = result.Where(a => a.Slug == @params.Slug);

        if (!string.IsNullOrWhiteSpace(@params.Title))
            result = result.Where(a => a.Title.Contains(@params.Title));

        if (@params.Id != null)
            result = result.Where(a => a.Id == @params.Id);

        var skip = (@params.PageId - 1) * @params.Take;
        var model = new ProductFilterResult
        {
            Data = await result.Skip(skip).Take(@params.Take).Select(a => a.MapListData()).ToListAsync(cancellationToken),
            FilterParams = @params,
        };
        model.GeneratePaging(result, @params.Take, @params.PageId);
        return model;
    }
}