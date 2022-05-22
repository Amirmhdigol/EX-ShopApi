using Common.Application;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories.DTOs;
namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    //Commands
    Task<OperationResult<long>> AddChild(AddCategoryChildCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult<long>> Create(CreateCategoryCommand command);
    Task<OperationResult> Remove(long id);

    //Queries
    Task<CategoryDto> GetCategoryById(long id);
    Task<List<CategoryChildDto>> GetCategoriesByParentId(long parentId);
    Task<List<CategoryDto>> GetCategoryList();
}