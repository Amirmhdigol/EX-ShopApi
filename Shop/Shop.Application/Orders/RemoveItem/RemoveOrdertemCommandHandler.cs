using Common.Application;
using Shop.Domain.OrderAgg;
namespace Shop.Application.Orders.RemoveItem;

public class RemoveOrdertemCommandHandler : IBaseCommandHandler<RemoveOrdertemCommand>
{
    private readonly IOrderRepository _repository;

    public RemoveOrdertemCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveOrdertemCommand request, CancellationToken cancellationToken)
    {
        var CurrentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if (CurrentOrder == null)
            return OperationResult.NotFound();

        CurrentOrder.RemoveItem(request.ItemId);
        await _repository.Save();
        return OperationResult.Success();
    }
}

