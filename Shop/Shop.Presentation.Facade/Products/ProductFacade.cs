using Common.Application;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;

namespace Shop.Presentation.Facade.Products;

public class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;
    private readonly ISellerInventoryFacade _inventoryFacade;
    public ProductFacade(IMediator mediator, ISellerInventoryFacade inventoryFacade, IDistributedCache cache)
    {
        _mediator = mediator;
        _inventoryFacade = inventoryFacade;
        _cache = cache;
    }

    public async Task<OperationResult> AddProductImage(AddProductImageCommand command)
    {
        var res = await _mediator.Send(command);
        if (res.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _cache.RemoveAsync(CacheKeys.Product(product.Slug));
        }
        return res;
    }

    public async Task<OperationResult> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        var res = await _mediator.Send(command);
        if (res.Status == OperationResultStatus.Success)
            await _cache.RemoveAsync(CacheKeys.Categories);
        return res;
    }

    public async Task<ProductFilterResult?> GetProductByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductDTO?> GetProductById(long productId)
    {
        return await _cache.GetOrSet(CacheKeys.Product(productId), () =>
        {
            return _mediator.Send(new GetProductByIdQuery(productId));
        });
    }

    public async Task<ProductDTO?> GetProductBySlug(string slug)
    {
        return await _cache.GetOrSet(CacheKeys.Product(slug), () =>
        {
            return _mediator.Send(new GetProductBySlugQuery(slug));
        });
    }

    public async Task<SingleProductForShopDTO> GetProductForShopSinglePageBySlug(string slug)
    {
        var product = await _mediator.Send(new GetProductBySlugQuery(slug));
        if (product == null) return null;
        var inventories = await _inventoryFacade.GetListByProductId(product.Id);
        return new SingleProductForShopDTO
        {
            Product = product,
            Inventories = inventories
        };
    }

    public async Task<ProductShopResult> GetProductsForShopByFilter(ProductShopFilterParam filterParams)
    {
        return await _mediator.Send(new GetProductsForShopQuery(filterParams));
    }

    public async Task<OperationResult> RemoveProductImage(RemoveProductImageCommand command)
    {
        var res = await _mediator.Send(command);
        if (res.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _cache.RemoveAsync(CacheKeys.Product(product.Slug));
        }
        return res;
    }
}