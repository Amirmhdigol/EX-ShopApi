global using Shop.Api.Infrastructure.Security;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Products;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CRUD_Product)]
public class ProductController : ApiController
{
    private readonly IProductFacade _facade;

    public ProductController(IProductFacade facade)
    {
        _facade = facade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetProductsByFilter([FromQuery] ProductFilterParams filterParams)
    {
        return QueryResult(await _facade.GetProductByFilter(filterParams));
    }

    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult>> GetProductsForShopByFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        return QueryResult(await _facade.GetProductsForShopByFilter(filterParams));
    }

    [HttpGet("{productId}")]
    public async Task<ApiResult<ProductDTO?>> GetProductById(long productId)
    {
        var product = await _facade.GetProductById(productId);
        return QueryResult(product);
    }
    
    [HttpGet("byslug/{slug}")]
    [AllowAnonymous]
    public async Task<ApiResult<ProductDTO?>> GetProductBySlug(string slug)
    {
        return QueryResult(await _facade.GetProductBySlug(slug));
    }

    [HttpPost]
    public async Task<ApiResult> CreateProduct([FromForm] CreateProductViewModel command)
    {
        return CommandResult(await _facade.Create(new CreateProductCommand
        {
            SeoData = command.SeoData.Map(),
            CategoryId = command.CategoryId,
            Description = command.Description,
            ImageFile = command.ImageFile,
            SecondrySubCategoryId = command.SecondarySubCategoryId,
            Slug = command.Slug,
            Specifications = command.GetSpecification(),
            SubCategoryId = command.SubCategoryId,
            Title = command.Title
        }));
    }

    [HttpPost("images")]
    public async Task<ApiResult> AddProductImage([FromForm] AddProductImageCommand command)
    {
        return CommandResult(await _facade.AddProductImage(command));
    }

    [HttpDelete("images")]
    public async Task<ApiResult> RemoveProductImage(RemoveProductImageCommand command)
    {
        return CommandResult(await _facade.RemoveProductImage(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditProduct([FromForm] EditProductViewModel command)
    {
        return CommandResult(await _facade.Edit(new EditProductCommand
        {
            CategoryId = command.CategoryId,
            Description = command.Description,
            SecondrySubCategoryId = command.SecondarySubCategoryId,
            SubCategoryId = command.SubCategoryId,
            ImageFile = command.ImageFile,
            ProductId = command.ProductId,
            SeoData = command.SeoData.Map(),
            Slug = command.Slug,
            Title = command.Title,
            Specifications = command.GetSpecification(),
        }));
    }
}
