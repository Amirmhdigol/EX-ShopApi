using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.Sliders.GetById;
using Shop.Query.SiteEntities.Sliders.GetList;

namespace Shop.Presentation.Facade.SiteEntities.Sliders;

internal class SliderFacade : ISliderFacade
{
    private readonly IMediator _mediator;
    public SliderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateSliderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSliderCommand command)
    {
        return await _mediator.Send(command);
    }
    
    public async Task<SliderDTO> GetSliderById(long sliderId)
    {
        return await _mediator.Send(new GetSliderByIdQuery(sliderId));
    }

    public async Task<List<SliderDTO>> GetSlidersList()
    {
        return await _mediator.Send(new GetSliderListQuery());
    }
}