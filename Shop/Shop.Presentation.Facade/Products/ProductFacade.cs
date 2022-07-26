using Common.Application;
using MediatR;
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
    private readonly ISellerInventoryFacade _inventoryFacade;
    public ProductFacade(IMediator mediator, ISellerInventoryFacade inventoryFacade)
    {
        _mediator = mediator;
        _inventoryFacade = inventoryFacade;
    }

    public async Task<OperationResult> AddProductImage(AddProductImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductFilterResult?> GetProductByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductDTO?> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDTO?> GetProductBySlug(string slug)
    {
        return await _mediator.Send(new GetProductBySlugQuery(slug));
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
        return await _mediator.Send(command);
    }
}