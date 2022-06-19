using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Delete;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.Banners.GetList;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Banners;

public class BannerFacade : IBannerFacade
{
    private readonly IMediator _mediator;
    public BannerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(long bannerId)
    {
        return await _mediator.Send(new DeleteBannerCommand(bannerId));
    }

    public async Task<OperationResult> Edit(EditBannerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<BannerDTO> GetBannerById(long bannerId)
    {
        return await _mediator.Send(new GetBannerByIdQuery(bannerId));
    }

    public async Task<List<BannerDTO>> GetBannersList()
    {
        return await _mediator.Send(new GetBannersListQuery());
    }
}
