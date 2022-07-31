using Common.Application;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.ShippingMethods.Delete;

public class DeleteShippingMethodCommandHandler : IBaseCommandHandler<DeleteShippingMethodCommand>
{
    private readonly IShippingMethodRepository _ShippingMethodRepository;
    public DeleteShippingMethodCommandHandler(IShippingMethodRepository sliderRepository)
    {
        _ShippingMethodRepository = sliderRepository;
    }

    public async Task<OperationResult> Handle(DeleteShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var slider = await _ShippingMethodRepository.GetTracking(request.Id);
        if (slider == null) return OperationResult.NotFound();

        _ShippingMethodRepository.Delete(slider);

        await _ShippingMethodRepository.Save();
        return OperationResult.Success();
    }
}
