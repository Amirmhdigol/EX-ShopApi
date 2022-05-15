using Common.Query;
using Shop.Query.SiteEntities.DTOs;
namespace Shop.Query.SiteEntities.Banners.GetList;

public record GetBannersListQuery : IQuery<List<BannerDTO>>;