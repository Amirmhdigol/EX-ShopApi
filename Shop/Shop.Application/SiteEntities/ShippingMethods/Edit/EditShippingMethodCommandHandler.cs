using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.ShippingMethods.Edit;

public class EditShippingMethodCommandHandler : IBaseCommandHandler<EditShippingMethodCommand>
{
    private readonly IShippingMethodRepository _repository;
    public EditShippingMethodCommandHandler(IShippingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shippingMethod = await _repository.GetTracking(request.Id);
        if (shippingMethod == null)
            return OperationResult.NotFound();

        shippingMethod.Edit(request.Title, request.Cost);
        await _repository.Save();
        return OperationResult.Success();
    }
}
