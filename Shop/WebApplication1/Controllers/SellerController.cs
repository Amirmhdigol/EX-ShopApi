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
    [PermissionChecker(Domain.RoleAgg.Permission.Seller_Management)]
    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        return CommandResult(await _sellerFacade.Create(command));
    }

    [HttpPut]
    [PermissionChecker(Domain.RoleAgg.Permission.Seller_Management)]
    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        return CommandResult(await _sellerFacade.Edit(command));
    }

    [HttpPost("addInventory")]
    [PermissionChecker(Domain.RoleAgg.Permission.Add_Inventory)]
    public async Task<ApiResult> AddSellerInventory(AddSellerInventoryCommand command)
    {
        return CommandResult(await _inventoryFacade.AddInventory(command));
    }

    [HttpPut("editInventory")]
    [PermissionChecker(Domain.RoleAgg.Permission.Edit_Inventory)]
    public async Task<ApiResult> EditSellerInventory(EditInventoryCommand command)
    {
        return CommandResult(await _inventoryFacade.EditInventory(command));
    }

    [HttpGet("{sellerId}")]
    public async Task<ApiResult<SellerDTO?>> GetSellerById(long sellerId)
    {
        return QueryResult(await _sellerFacade.GetSellerById(sellerId));
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<ApiResult<SellerDTO?>> GetSellerByUserId()
    {
        return QueryResult(await _sellerFacade.GetSellerByUserId(User.GetUserId()));
    }

    [HttpGet]
    [PermissionChecker(Domain.RoleAgg.Permission.Seller_Management)]
    public async Task<ApiResult<SellerFilterResult>> GetSellersByFilter(SellerFilterParams filterParams)
    {
        return QueryResult(await _sellerFacade.GetSellersByFilter(filterParams));
    }

    [HttpGet("inventory")]
    [PermissionChecker(Domain.RoleAgg.Permission.Seller_Management)]
    public async Task<ApiResult<List<InventoryDTO>>> GetSellerInventories()
    {
        var seller = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        if (seller == null) return QueryResult(new List<InventoryDTO>());

        return QueryResult(await _inventoryFacade.GetList(seller.Id));
    }

    [HttpGet("Inventory/{inventoryId}")]
    [PermissionChecker(Domain.RoleAgg.Permission.Seller_Management)]
    public async Task<ApiResult<InventoryDTO>> GetSellerInventoryById(long inventoryId)
    {
        var seller = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        if (seller == null) return QueryResult(new InventoryDTO());

        var result = await _inventoryFacade.GetById(inventoryId);

        if (result == null || result.SellerId != seller.Id) return QueryResult(new InventoryDTO());

        return QueryResult(result);
    }
}