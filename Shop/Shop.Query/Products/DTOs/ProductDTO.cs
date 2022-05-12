using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Domain.ProductAgg;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products.DTOs;

public class ProductDTO : BaseDTO
{
    public string Title { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
    public ProductCategoryDTO Category { get; set; }
    public ProductCategoryDTO SubCategory { get; set; }
    public ProductCategoryDTO? SecondrySubCategory { get; set; }
    public string Slug { get; set; }
    public SeoData SeoData { get; set; }
    public List<ProductImageDTO> Images { get; set; }
    public List<ProductSpecificationsDTO> Specifications { get; set; }
}
public class ProductCategoryDTO 
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public string Title { get; set; }
    public SeoData SeoData { get; set; }
    public string Slug { get; set; }
}