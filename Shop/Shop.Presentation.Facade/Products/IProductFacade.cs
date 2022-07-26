using Common.Application;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Query.Products.DTOs;
using Shop.Query.Sellers.DTOs;

namespace Shop.Presentation.Facade.Products;
public interface IProductFacade
{
    //Comands
    Task<OperationResult> Create(CreateProductCommand command);
    Task<OperationResult> Edit(EditProductCommand command);
    Task<OperationResult> AddProductImage(AddProductImageCommand command);
    Task<OperationResult> RemoveProductImage(RemoveProductImageCommand command);

    //Queries
    Task<ProductDTO?> GetProductById(long productId);
    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);
    Task<ProductShopResult> GetProductsForShopByFilter(ProductShopFilterParam filterParams);
    Task<ProductDTO?> GetProductBySlug(string slug);
    Task<SingleProductForShopDTO> GetProductForShopSinglePageBySlug(string slug);
}

public class SingleProductForShopDTO
{                               
    public ProductDTO Product { get; set; }
    public List<InventoryDTO> Inventories { get; set; }
}                                           