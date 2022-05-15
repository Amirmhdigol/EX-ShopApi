using Common.Query;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.GetByFilter;
public class GetBySellerFilterQuery : QueryFilter<SellerFilterResult, SellerFilterParams>
{
    public GetBySellerFilterQuery(SellerFilterParams filterParams) : base(filterParams)
    {
    }
}
