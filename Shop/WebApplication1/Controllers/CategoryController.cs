                                                                                                                                                                                                                                        using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories.DTOs;
using System.Net;
namespace Shop.Api.Controllers;
public class CategoryController : ApiController
{
    private readonly ICategoryFacade _facade;
    public CategoryController(ICategoryFacade facade)
    {
        _facade = facade;
    }

    [HttpGet]
    public async Task<ApiResult<List<CategoryDto>>> GetCategoriesList()
    {
        var result = await _facade.GetCategoryList();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<CategoryDto>> GetCategoryById(long id)
    {
        var result = await _facade.GetCategoryById(id);
        return QueryResult(result);
    }

    [HttpGet("getChilds/{parentId}")]
    public async Task<ApiResult<List<CategoryChildDto>>> GetCategoryByParentId(long parentId)
    {
        var result = await _facade.GetCategoriesByParentId(parentId);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _facade.Create(command);
        var url = Url.Action("GetCategoryById", "Category", new { id = result.Data });
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPost("AddChild")]
    public async Task<ApiResult<long>> CreateChildCategory(AddCategoryChildCommand command)
    {
        var result = await _facade.AddChild(command);
        var url = Url.Action("GetCategoryById", "Category", new { id = result.Data });
        return CommandResult(result, HttpStatusCode.Created, url);
    }

    [HttpPut]
    public async Task<ApiResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _facade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("{categoryId}")]
    public async Task<ApiResult> DeleteCategory(long categoryId)
    {
        var result = await _facade.Remove(categoryId);
        return CommandResult(result);
    }
}