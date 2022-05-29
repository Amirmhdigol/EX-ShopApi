global using Common.Query;
global using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers.GetByUserId;

public record GetSellerByUserIdQuery(long UserId) : IQuery<SellerDTO>;
