using Common.Application;
using Shop.Application.Categories.AddChild;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories.DTOs;
namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    //Commands
    Task<OperationResult> AddChild(AddCategoryChildCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> Create(CreateCategoryCommand command);

    //Queries
    Task<CategoryDto> GetCategoryById(long id);
    Task<List<CategoryChildDto>> GetCategoriesByParentId(long parentId);
    Task<List<CategoryDto>> GetCategoryList();
}