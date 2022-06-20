using Common.Application;
using Shop.Domain.CategoryAgg;

namespace Shop.Application.Categories.Remove;

internal class RemoveCategoryCommandHandler : IBaseCommandHandler<RemoveCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    public RemoveCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteCategory(request.CategoryId);
        if (result)
        {
            await _repository.Save();
            return OperationResult.Success();
        }
        return OperationResult.Error("You cannot delete this category");
    }
}