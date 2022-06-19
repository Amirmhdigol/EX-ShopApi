using Common.Application;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Query.SiteEntities.DTOs;
namespace Shop.Presentation.Facade.SiteEntities.Banners;
public interface IBannerFacade
{
    Task<OperationResult> Create(CreateBannerCommand command);
    Task<OperationResult> Edit(EditBannerCommand command);
    Task<OperationResult> Delete(long bannerId);

    Task<BannerDTO> GetBannerById(long bannerId);
    Task<List<BannerDTO>> GetBannersList();
}