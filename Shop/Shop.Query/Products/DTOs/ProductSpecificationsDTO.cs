using Common.Query;

namespace Shop.Query.Products.DTOs;

public class ProductSpecificationsDTO : BaseDTO
{
    public string Key { get; set; }
    public string Value { get; set; }
}
