using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Sellers.DTOs;

namespace Shop.Api.Controllers;

public class SellerController : ApiController
{
    private readonly ISellerFacade _sellerFacade;
    private readonly ISellerInventoryFacade _inventoryFacade;
    public SellerController(ISellerFacade facade, ISellerInventoryFacade inventoryFacade)
    {
        _sellerFacade = facade;
        _inventoryFacade = inventoryFacade;
    }

    [HttpPost]
    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        return CommandResult(await _sellerFacade.Create(command));
    }

    [HttpPut]
    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        return CommandResult(await _sellerFacade.Edit(command));
    }

    [HttpPost("addInventory")]
    public async Task<ApiResult> AddSellerInventory(AddSellerInventoryCommand command)
    {
        return CommandResult(await _inventoryFacade.AddInventory(command));
    }

    [HttpPut("editInventory")]
    public async Task<ApiResult> EditSellerInventory(EditInventoryCommand command)
    {
        return CommandResult(await _inventoryFacade.EditInventory(command));
    }

    [HttpGet("{sellerId}")]
    public async Task<ApiResult<SellerDTO?>> GetSellerById(long sellerId)
    {
        return QueryResult(await _sellerFacade.GetSellerById(sellerId));
    }

    [HttpGet]
    public async Task<ApiResult<SellerFilterResult>> GetSellersByFilter(SellerFilterParams filterParams)
    {
        return QueryResult(await _sellerFacade.GetSellersByFilter(filterParams));
    }
}
