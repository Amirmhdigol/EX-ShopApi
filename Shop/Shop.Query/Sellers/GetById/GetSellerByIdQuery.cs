using Common.Query;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers.GetById;
public record GetSellerByIdQuery(long SellerId) : IQuery<SellerDTO>;