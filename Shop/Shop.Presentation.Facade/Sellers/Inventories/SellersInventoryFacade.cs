using Common.Application;
using MediatR;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.EditInventory;

namespace Shop.Presentation.Facade.Sellers.Inventories;

public class SellersInventoryFacade : ISellerInventoryFacade
{
    private readonly IMediator _mediator;
    public SellersInventoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddInventory(AddSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditInventory(EditInventoryCommand command)
    {
        return await _mediator.Send(command);
    }
}