using Shop.Domain.SellerAgg;
using Shop.Query.Sellers.DTOs;
namespace Shop.Query.Sellers;
public static class SellerMapper
{
    public static SellerDTO Map(this Seller seller)
    {
        if (seller == null) return null;
        return new()
        {
            CreationDate = seller.CreationDate,
            Id = seller.Id,
            ShopName = seller.ShopName,
            NationalCode = seller.NationalCode,
            Status = seller.Status,
            UserId = seller.UserId,
        };
    }

}