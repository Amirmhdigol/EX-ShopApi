using Common.Application;
using Shop.Application.SiteEntities.Sliders.Create;
using Shop.Application.SiteEntities.Sliders.Edit;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Presentation.Facade.SiteEntities.Sliders;
public interface ISliderFacade
{
    Task<OperationResult> Create(CreateSliderCommand command);
    Task<OperationResult> Edit(EditSliderCommand command);

    Task<SliderDTO> GetSliderById(long sliderId);
    Task<List<SliderDTO>> GetSlidersList();
}
