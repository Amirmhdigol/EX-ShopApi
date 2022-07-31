using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Delete;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Query.SiteEntities.ShippingMethods.DTOs;
using Shop.Query.SiteEntities.ShippingMethods.GetById;
using Shop.Query.SiteEntities.ShippingMethods.GetList;

namespace Shop.Presentation.Facade.ShippingMethods;

public class ShippingMethodFacade : IShippingMethodFacade
{
    private readonly IMediator _mediator;
    public ShippingMethodFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateShippingMethodCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long id)
    {
        return await _mediator.Send(new DeleteShippingMethodCommand(id));
    }

    public async Task<OperationResult> Edit(EditShippingMethodCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ShippingMethodDTO> GetShippingMethodById(long id)
    {
        return await _mediator.Send(new GetShippingMethodByIdQuery(id));
    }

    public async Task<List<ShippingMethodDTO>> GetShippingMethodList()
    {
        return await _mediator.Send(new GetShippingMethodsListQuery());
    }
}
