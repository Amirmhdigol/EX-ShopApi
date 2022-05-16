using Common.Application;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Presentation.Facade.Products;
using Shop.Query.Sellers.DTOs;
namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{
    //Commands
    Task<OperationResult> Create(CreateSellerCommand command);
    Task<OperationResult> Edit(EditSellerCommand command);

    //Queries
    Task<SellerDTO?> GetSellerById(long sellerId);
    Task<SellerFilterResult> GetSellersByFilter(SellerFilterParams filterParams);
}
