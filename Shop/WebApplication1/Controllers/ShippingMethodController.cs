using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Presentation.Facade.ShippingMethods;
using Shop.Query.SiteEntities.ShippingMethods.DTOs;

namespace Shop.Api.Controllers;

[Authorize]
public class ShippingMethodController : ApiController
{
    private readonly IShippingMethodFacade _facade;
    public ShippingMethodController(IShippingMethodFacade facade)
    {
        _facade = facade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<ShippingMethodDTO>>> GetShippingMethods()
    {
        var result = await _facade.GetShippingMethodList();
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{Id}")]
    public async Task<ApiResult<ShippingMethodDTO>> GetShippingMethodById(long Id)
    {
        var result = await _facade.GetShippingMethodById(Id);
        return QueryResult(result);
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ApiResult> CreateShippingMethod(CreateShippingMethodCommand command)
    {
        var result = await _facade.Create(command);
        return CommandResult(result);
    }
    
    [AllowAnonymous]
    [HttpPut]
    public async Task<ApiResult> EditShippingMethod(EditShippingMethodCommand command)
    {
        var result = await _facade.Edit(command);
        return CommandResult(result);
    } 
    
    [AllowAnonymous]
    [HttpDelete("Id")]
    public async Task<ApiResult> DeleteShippingMethod(long Id)
    {
        var result = await _facade.Delete(Id);
        return CommandResult(result);
    }
}
