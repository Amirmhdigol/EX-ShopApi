using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;
public class ProductController : ApiController
{
    private readonly IProductFacade _facade;

    public ProductController(IProductFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetProductsByFilter([FromQuery] ProductFilterParams filterParams)
    {
        return QueryResult(await _facade.GetProductByFilter(filterParams));
    }

    [HttpGet("{productId}")]
    public async Task<ApiResult<ProductDTO?>> GetProductsById(long productId)
    {
        return QueryResult(await _facade.GetProductById(productId));
    }

    [HttpGet("{slug}")]
    public async Task<ApiResult<ProductDTO?>> GetProductBySlug(string slug)
    {
        return QueryResult(await _facade.GetProductBySlug(slug));
    }

    [HttpPost]
    public async Task<ApiResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        return CommandResult(await _facade.Create(command));
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
    public async Task<ApiResult> EditProduct([FromForm] EditProductCommand command)
    {
        return CommandResult(await _facade.Edit(command));
    }
}
